using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class ClockRepository : IRepository<Clock>, IRepositoryAsync<Clock>
    {
        private readonly ApplicationDbContext _db;

        public ClockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Clock> GetAll()
        {
            return _db.Clocks
                .Include(c => c.City)
                .Include(c => c.ClockType)
                .ToList();
        }

        public Clock Get(int id)
        {
            return _db.Clocks.Find(id);
        }

        public void Add(Clock item)
        {
            _db.Clocks.Add(item);
        }

        public void Update(Clock item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var clock = _db.Clocks.Find(id);
            if (clock != null)
                _db.Clocks.Remove(clock);
        }


        public Task<List<Clock>> GetAllAsync()
        {
            return _db.Clocks
                .Include(c => c.City)
                .Include(c => c.ClockType)
                .ToListAsync();
        }

        public Task<Clock> GetAsync(int id)
        {
            return _db.Clocks.FindAsync(id);
        }

        public async Task AddAsync(Clock item)
        {
            await _db.Clocks.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var clock = await _db.Clocks.FindAsync(id);
            if (clock != null)
                _db.Clocks.Remove(clock);
        }

        public Task<bool> AnyAsync()
        {
            return _db.Clocks.AnyAsync();
        }
    }
}