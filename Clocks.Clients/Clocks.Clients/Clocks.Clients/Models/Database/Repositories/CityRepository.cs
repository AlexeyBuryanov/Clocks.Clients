using Clocks.Clients.Core.Models.Database.Repositories.Base;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class CityRepository : RepositoryAsync<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext db) 
            : base(db)
        {
        }

        public ApplicationDbContext ApplicationDbContext
            => DbContext as ApplicationDbContext;
    }
}