using Clocks.Clients.Core.Models.Database.Repositories;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Models.Database
{
    internal class UnitOfWork : IDisposable
    {
        private static readonly ApplicationDbContext Context = 
            new ApplicationDbContext(DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME));
        
        private CityRepository _cityRepository;
        private ClockTypeRepository _clockTypeRepository;
        private ClockRepository _clockRepository;

        public CityRepository CityRepository 
            => _cityRepository ?? (_cityRepository = new CityRepository(Context));
        public ClockTypeRepository ClockTypeRepository 
            => _clockTypeRepository ?? (_clockTypeRepository = new ClockTypeRepository(Context));
        public ClockRepository ClockRepository 
            => _clockRepository ?? (_clockRepository = new ClockRepository(Context));

        public int Save() => Context.SaveChanges();
        public async Task<int> SaveAsync() => await Context.SaveChangesAsync();
        public async Task EnsureCreatedAsync() => await Context.Database.EnsureCreatedAsync();

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
