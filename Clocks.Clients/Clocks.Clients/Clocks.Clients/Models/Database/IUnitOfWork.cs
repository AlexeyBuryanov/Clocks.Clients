using System;
using System.Threading.Tasks;
using Clocks.Clients.Core.Models.Database.Repositories;

namespace Clocks.Clients.Core.Models.Database
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IClockTypeRepository ClockTypes { get; }
        IClockRepository Clocks { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}