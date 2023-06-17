using Infrastructure.Common.Logging;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogError(ex);
                throw;
            }
        }
    }
}
