namespace Domain.Entities
{
    public class Inquiry : BaseEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<InquiryCoverage> InquiryCoverages { get; set; } = default!;

    }
}
