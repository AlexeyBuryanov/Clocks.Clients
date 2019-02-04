using Clocks.Clients.Core.Models.Database.Repositories;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clocks.Clients.Core.Models.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly Lazy<UnitOfWork> Lazy =
            new Lazy<UnitOfWork>(() => 
                new UnitOfWork(new ApplicationDbContext(
                    DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME))));

        private readonly ApplicationDbContext _applicationDbContext;

        private UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Cities = new CityRepository(_applicationDbContext);
            ClockTypes = new ClockTypeRepository(_applicationDbContext);
            Clocks = new ClockRepository(_applicationDbContext);
        }

        public static UnitOfWork Instance => Lazy.Value;


        public ICityRepository Cities { get; }
        public IClockTypeRepository ClockTypes { get; }
        public IClockRepository Clocks { get; }


        public int SaveChanges() 
            => _applicationDbContext.SaveChanges();

        public Task<int> SaveChangesAsync() 
            => _applicationDbContext.SaveChangesAsync();

        public async Task EnsureCreatedAsync() 
            => await _applicationDbContext.Database.EnsureCreatedAsync();


        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
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
