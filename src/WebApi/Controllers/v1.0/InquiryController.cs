using Application.Common.Models.Response.Base;
using Application.Common.Models.Response;
using Infrastructure.Common.Configuration;
using Infrastructure.Common.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers.Base;
using WebApi.Filters;
using Application.Common.Interfaces.BusinessRules;
using Application.Common.Models.Request;

namespace WebApi.Controllers.v1_0
{

    [ApiVersion("1.0")]
    public class InquiryController : ApiControllerBase
    {
        private readonly IInquiryBusinessRule _service;

        public InquiryController(
            IOptionsMonitor<ApplicationSetting> options,
            ILogger<InquiryController> logger,
            IHttpContextAccessor accessor,
            IInquiryBusinessRule service
        ) : base(options, accessor)
        {
            _service = service;
        }

        [HttpPost, MapToApiVersion("1.0"), ValidateModel]
        public async Task<IActionResult> Inquiry([FromBody] InquiryRequest input)
        {
            var resp = new ResponseBase<InquiryResponse>();
            try
            {
                resp = await _service.Inquiry(input);
            }
            catch (Exception ex)
            {
                LogUtility.LogError(ex);
                resp.ResponseCode = Application.Constants.Response.SystemError;
            }
            return HandleResponse(resp);
        }

       
    }
}