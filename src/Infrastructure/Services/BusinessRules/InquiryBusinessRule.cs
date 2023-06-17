using Application.Common.Interfaces.BusinessRules;
using Application.Common.Models.Request;
using Application.Common.Models.Response;
using Application.Common.Models.Response.Base;
using Application.Constants;
using Infrastructure.Common.Configuration;
using Infrastructure.Common.Logging;
using Infrastructure.Persistence;
using Infrastructure.Services.BusinessRules.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Domain.Entities;

namespace Infrastructure.Services.BusinessRules
{
    public class InquiryBusinessRule : BusinessRuleBase, IInquiryBusinessRule
    {
        public InquiryBusinessRule(
           IOptionsMonitor<ApplicationSetting> options,
           ApplicationDbContext context,
           IHttpContextAccessor accessor
       ) : base(options, context, accessor) { }

        public InquiryBusinessRule(
            IOptionsMonitor<ApplicationSetting> options,
            ApplicationDbContext context
        ) : base(options, context) { }

        private InquiryValidationResponse Validate(InquiryRequest input, List<Coverage> coverages)
        {
            if (input.Coverages.Any() == false)
                return InquiryValidationResponse.EmptyCoverage;
            if (input.Coverages.GroupBy(c => c.Type).Any(c => c.Count() > 1))
                return InquiryValidationResponse.DuplicatedSelection;
            var f = false;
            input.Coverages.ForEach(c =>
            {
                Coverage coverage = coverages.First(item => item.Type == c.Type);
                if (c.RequestedFund < coverage.MinimumFund || c.RequestedFund > coverage.MaximumFund)
                    f = true;
            });
            if (f)
                return InquiryValidationResponse.InvalidRequestedFund;
            return InquiryValidationResponse.IsValid;
        }



        public async Task<ResponseBase<InquiryResponse>> Inquiry(InquiryRequest input)
        {
            var response = new ResponseBase<InquiryResponse>();
            try
            {
                if (input is null || input.Coverages is null)
                {
                    response.ResponseCode = Response.InvalidParameters;
                    return response;
                }

                var coverages = await Context.Coverages.Where(c => input.Coverages.Select(rc => rc.Type).Distinct().Contains(c.Type)).ToListAsync();

                var isValid = Validate(input, coverages);
                if (isValid != InquiryValidationResponse.IsValid)
                {
                    response.ResponseCode = Response.FromValidationResponse(isValid);
                    return response;
                }


                var inquiry = new Inquiry
                {
                    Date = DateTime.Now,
                    Title = input.Title,
                    InquiryCoverages = new List<InquiryCoverage>(),
                };
                response.Data = new InquiryResponse
                {
                    Date = inquiry.Date,
                    Title = inquiry.Title,
                    InquiryCoverages = new List<InquiryCoverageResponse>(),
                };

                input.Coverages.ForEach(rc =>
                {
                    var c = coverages.First(c => c.Type == rc.Type);
                    inquiry.InquiryCoverages.Add(new InquiryCoverage
                    {
                        CoverageId = c.Id,
                        RequestedFund = rc.RequestedFund,
                    });
                    response.Data.InquiryCoverages.Add(new InquiryCoverageResponse
                    {
                        Type = rc.Type,
                        RequestedFund = rc.RequestedFund,
                        InsurancePremium = Math.Ceiling(rc.RequestedFund * (decimal)c.InsuranceCoefficient)
                    });
                });

                await Context.Inquiries.AddAsync(inquiry);
                await Context.SaveChangesAsync();





                response.ResponseCode = Response.SucceededCreated;
                return response;
            }
            catch (Exception ex)
            {
                LogUtility.LogError(ex);
                response.ResponseCode = Response.SystemError;
                return response;
            }
        }
    }
}
