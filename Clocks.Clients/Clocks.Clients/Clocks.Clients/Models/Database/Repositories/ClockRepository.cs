using Clocks.Clients.Core.Models.Database.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class ClockRepository : RepositoryAsync<Clock>, IClockRepository
    {
        public ClockRepository(ApplicationDbContext db) 
            : base(db)
        {
        }

        public ApplicationDbContext ApplicationDbContext
            => DbContext as ApplicationDbContext;

        public Task<List<Clock>> GetAllWithIncludesAsync()
        {
            return ApplicationDbContext.Clocks
                .Include(c => c.City)
                .Include(c => c.ClockType)
                .ToListAsync();
        }
    }
}