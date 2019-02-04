using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clocks.Clients.Core.Models.Database.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        void Add(T item);
        void AddRange(IEnumerable<T> items);

        void Update(T item);
        void UpdateRange(IEnumerable<T> items);

        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);

        bool Any();
    }
}