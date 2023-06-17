using Application.Resources;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models.Request
{
    public class InquiryRequest
    {
        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Messages))]
        [MaxLength(250, ErrorMessageResourceName = "MaxLen", ErrorMessageResourceType = typeof(Messages))]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Messages))]
        public List<InquiryCoverageRequest> Coverages { get; set; } = default!;
    }

    public class InquiryCoverageRequest
    {
        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Messages))]
        public CoverageType Type { get; set; }
        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Messages))]
        public decimal RequestedFund { get; set; }
    }
}