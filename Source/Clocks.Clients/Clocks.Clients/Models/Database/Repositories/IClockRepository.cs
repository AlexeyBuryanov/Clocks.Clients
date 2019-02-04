using Clocks.Clients.Core.Models.Database.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public interface IClockRepository : IRepositoryAsync<Clock>
    {
        Task<List<Clock>> GetAllWithIncludesAsync();
    }
}