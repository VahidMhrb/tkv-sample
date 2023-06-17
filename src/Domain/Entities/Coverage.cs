namespace Domain.Entities
{
    public class Coverage : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinimumFund { get; set; }
        public decimal MaximumFund { get; set; }
        public double InsuranceCoefficient { get; set; }
        public CoverageType Type { get; set; }
    }
}
