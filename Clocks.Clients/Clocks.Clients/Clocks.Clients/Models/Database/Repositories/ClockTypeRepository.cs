using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public class ClockTypeRepository : IRepository<ClockType>, IRepositoryAsync<ClockType>
    {
        private readonly ApplicationDbContext _db;

        public ClockTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<ClockType> GetAll()
        {
            return _db.ClockTypes.ToList();
        }

        public ClockType Get(int id)
        {
            return _db.ClockTypes.Find(id);
        }

        public void Add(ClockType item)
        {
            _db.ClockTypes.Add(item);
        }

        public void Update(ClockType item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var type = _db.ClockTypes.Find(id);
            if (type != null)
                _db.ClockTypes.Remove(type);
        }


        public Task<List<ClockType>> GetAllAsync()
        {
            return _db.ClockTypes.ToListAsync();
        }

        public Task<ClockType> GetAsync(int id)
        {
            return _db.ClockTypes.FindAsync(id);
        }

        public async Task AddAsync(ClockType item)
        {
            await _db.ClockTypes.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var clockType = await _db.ClockTypes.FindAsync(id);
            if (clockType != null)
                _db.ClockTypes.Remove(clockType);
        }

        public Task<bool> AnyAsync()
        {
            return _db.ClockTypes.AnyAsync();
        }
    }
}