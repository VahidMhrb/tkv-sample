using Application.Common.Attributes;
using System.ComponentModel;
using System.Net;

namespace Application.Constants
{
    public class Response
    {
        [Description("عملیات با موفقیت انجام شد")]
        [HttpStatus(HttpStatusCode.OK)]
        [ActionCode("00000")]
        public const int SucceededOk = 0;

        [Description("عملیات با موفقیت انجام شد")]
        [HttpStatus(HttpStatusCode.Created)]
        [ActionCode("00000")]
        public const int SucceededCreated = 1;

        [Description("خطای سیستمی رخ داده است.")]
        [HttpStatus(HttpStatusCode.InternalServerError)]
        [ActionCode("00002")]
        public const int SystemError = 2;

        [Description("پارامترهای ورودی معتبر نمی باشد.")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        [ActionCode("00003")]
        public const int InvalidParameters = 3;

        [Description("داده ای برای انجام عملیات درخواستی یافت نشد.")]
        [HttpStatus(HttpStatusCode.NotFound)]
        [ActionCode("00004")]
        public const int NotFound = 4;

        [Description("تنظیمات سامانه یافت نشد")]
        [HttpStatus(HttpStatusCode.InternalServerError)]
        [ActionCode("00005")]
        public const int ApplicationSettingNotFound = 5;

        [Description("عملیات درخواستی مجاز نمی باشد")]
        [HttpStatus(HttpStatusCode.Forbidden)]
        [ActionCode("00006")]
        public const int InvalidOperation = 6;

        [Description("مقادیر ارسالی تکراری می باشد.")]
        [HttpStatus(HttpStatusCode.Conflict)]
        [ActionCode("00007")]
        public const int DuplicateData = 7;

        [Description("شما دسترسی به انجام عملیات درخواستی را ندارید")]
        [HttpStatus(HttpStatusCode.Unauthorized)]
        [ActionCode("00008")]
        public const int Unauthorized = 8;

        [Description("برای انجام عملیات درخواستی باید وارد سامانه شده باشید")]
        [HttpStatus(HttpStatusCode.Unauthorized)]
        [ActionCode("00009")]
        public const int NotLogedIn = 9;

        [Description("دسترسی انجام عملیات درخواستی از شما گرفته شده است")]
        [HttpStatus(HttpStatusCode.Forbidden)]
        [ActionCode("00010")]
        public const int Forbidden = 10;

        [Description("منبع مورد نظر جهت دسترسی مشخص نشده است")]
        [HttpStatus(HttpStatusCode.NotFound)]
        [ActionCode("00011")]
        public const int InvalidResourceIdOnCheckPermission = 11;

        [Description("پوشش به درستی وارد نشده است.")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        [ActionCode("00012")]
        public const int EmptyCoverage = 12;

        [Description("داده تکراری وارد شده است")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        [ActionCode("00013")]
        public const int DuplicatedSelection = 13;

        [Description("مبلغ پوشش بیشتر/کمتر از مبلغ مجاز پوشش می باشد")]
        [HttpStatus(HttpStatusCode.BadRequest)]
        [ActionCode("00014")]
        public const int InvalidRequestedFund = 14;






        public static int FromValidationResponse(InquiryValidationResponse input) =>
        input switch
        {
            InquiryValidationResponse.IsValid => SucceededOk,
            InquiryValidationResponse.EmptyCoverage => EmptyCoverage,
            InquiryValidationResponse.DuplicatedSelection => DuplicatedSelection,
            InquiryValidationResponse.InvalidRequestedFund => InvalidRequestedFund,
            _ => throw new NotImplementedException(),
        };

    }
}
