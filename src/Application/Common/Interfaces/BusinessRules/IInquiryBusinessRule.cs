using Application.Common.Models.Response.Base;
using Application.Common.Models.Response;
using Application.Common.Models.Request;

namespace Application.Common.Interfaces.BusinessRules
{
    public interface IInquiryBusinessRule
    {
        Task<ResponseBase<InquiryResponse>> Inquiry(InquiryRequest input);
    }
}
