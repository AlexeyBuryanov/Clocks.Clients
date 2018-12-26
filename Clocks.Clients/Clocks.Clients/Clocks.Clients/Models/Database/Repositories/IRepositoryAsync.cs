using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T item);
        Task DeleteAsync(int id);
        Task<bool> AnyAsync();
    }
}