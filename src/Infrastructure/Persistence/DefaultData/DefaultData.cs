using Domain.Entities;


namespace Infrastructure.Persistence.DefaultData
{
    public static class DefaultData
    {
        public static List<Coverage> Coverage => new()
        {
            new Coverage {
                Id = 1,
                Name = "Surgical",
                Description = "Surgical Coverage",
                Type = Domain.Enums.CoverageType.Surgical,
                MinimumFund = 5000,
                MaximumFund = 500000000,
                InsuranceCoefficient = 0.0052,
            },
            new Coverage {
                Id = 2,
                Name = "Dental",
                Description = "Dental Coverage",
                Type = Domain.Enums.CoverageType.Dental,
                MinimumFund = 4000,
                MaximumFund = 400000000,
                InsuranceCoefficient = 0.0042,
            },
            new Coverage {
                Id = 3,
                Name = "Hospitalization",
                Description = "Hospitalization Coverage",
                Type = Domain.Enums.CoverageType.Hospitalization,
                MinimumFund = 2000,
                MaximumFund = 200000000,                
                InsuranceCoefficient = 0.005,
            },
        };
    }
}
