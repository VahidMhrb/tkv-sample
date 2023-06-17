using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence.Configurations
{
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(i => i.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(i => i.Date)
                .HasDefaultValueSql("(getdate())")
                .IsRequired();

            builder.HasMany(i => i.InquiryCoverages)
                .WithOne(ic => ic.Inquiry)                
                .IsRequired();
        }
    }
}
