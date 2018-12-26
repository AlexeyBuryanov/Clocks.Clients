using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class CityRepository : IRepository<City>, IRepositoryAsync<City>
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<City> GetAll()
        {
            return _db.Cities.ToList();
        }

        public City Get(int id)
        {
            return _db.Cities.Find(id);
        }

        public void Add(City item)
        {
            _db.Cities.Add(item);
        }

        public void Update(City item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var city = _db.Cities.Find(id);
            if (city != null)
                _db.Cities.Remove(city);
        }


        public Task<List<City>> GetAllAsync()
        {
            return _db.Cities.ToListAsync();
        }

        public Task<City> GetAsync(int id)
        {
            return _db.Cities.FindAsync(id);
        }

        public async Task AddAsync(City item)
        {
            await _db.Cities.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _db.Cities.FindAsync(id);
            if (city != null)
                _db.Cities.Remove(city);
        }

        public Task<bool> AnyAsync()
        {
            return _db.Cities.AnyAsync();
        }
    }
}