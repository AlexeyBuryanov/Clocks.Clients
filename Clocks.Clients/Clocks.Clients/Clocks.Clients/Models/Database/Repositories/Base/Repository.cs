using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Clocks.Clients.Core.Models.Database.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public T Get(int id)
        {
            return DbContext.Set<T>()
                .Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>()
                .ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>()
                .Where(predicate)
                .ToList();
        }

        public void Add(T item)
        {
            DbContext.Set<T>()
                .Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            DbContext.Set<T>()
                .AddRange(items);
        }

        public void Update(T item)
        {
            DbContext.Entry(item)
                .State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            DbContext.Set<T>()
                .UpdateRange(items);
        }

        public void Delete(T item)
        {
            DbContext.Set<T>()
                .Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            DbContext.Set<T>()
                .RemoveRange(items);
        }

        public bool Any()
        {
            return DbContext.Set<T>()
                .Any();
        }
    }
}
