namespace Domain.Entities
{
    public class InquiryCoverage : BaseEntity<int>
    {
        public int InquiryId { get; set; }
        public int CoverageId { get; set; }
        public decimal RequestedFund { get; set; }
        public Inquiry Inquiry { get; set; } = default!;
        public Coverage Coverage { get; set; } = default!;
    }
}
