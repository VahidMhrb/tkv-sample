using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class CoverageConfiguration : IEntityTypeConfiguration<Coverage>
    {
        public void Configure(EntityTypeBuilder<Coverage> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(c => c.MinimumFund)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(c => c.MaximumFund)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(c => c.InsuranceCoefficient)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(c => c.Type)
                .IsRequired();
        }
    }
}
