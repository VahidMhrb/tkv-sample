using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Coverage> Coverages { get; }
        DbSet<Inquiry> Inquiries { get; }
        DbSet<InquiryCoverage> InquiryCoverages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
