using Clocks.Clients.Core.Models.Database.Repositories.Base;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class ClockTypeRepository : RepositoryAsync<ClockType>, IClockTypeRepository
    {
        public ClockTypeRepository(ApplicationDbContext db) 
            : base(db)
        {
        }

        public ApplicationDbContext ApplicationDbContext 
            => DbContext as ApplicationDbContext;
    }
}