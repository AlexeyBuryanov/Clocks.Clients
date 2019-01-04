using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models.Database.Repositories.Base
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);

        Task UpdateAsync(T item);
        Task UpdateRangeAsync(IEnumerable<T> items);

        Task DeleteAsync(T item);
        Task DeleteRangeAsync(IEnumerable<T> items);

        Task<bool> AnyAsync();
    }
}