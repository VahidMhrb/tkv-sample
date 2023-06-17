using Domain.Enums;

namespace Application.Common.Models.Response
{
    public class InquiryResponse
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal SumInsurancePremium { get { return InquiryCoverages.Sum(i => i.InsurancePremium); } }
        public List<InquiryCoverageResponse> InquiryCoverages { get; set; } = default!;
    }

    public class InquiryCoverageResponse
    {        
        public CoverageType Type { get; set; }
        public decimal RequestedFund { get; set; }
        public decimal InsurancePremium { get; set; }
    }
}
