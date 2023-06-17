using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class InquiryCoverageConfiguration : IEntityTypeConfiguration<InquiryCoverage>
    {
        public void Configure(EntityTypeBuilder<InquiryCoverage> builder)
        {
            builder.HasKey(ic => ic.Id);
            builder.Property(ic => ic.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(ic => ic.InquiryId)
                .IsRequired();
            builder.HasOne(ic => ic.Inquiry)
                .WithMany(i => i.InquiryCoverages)
                .IsRequired();

            builder.Property(ic => ic.CoverageId)
                .IsRequired();
            builder.HasOne(ic => ic.Coverage);

            builder.Property(i => i.RequestedFund)
                .HasColumnType("money")
                .IsRequired();
        }
    }
}
