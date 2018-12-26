using System.Collections.Generic;

namespace Clocks.Clients.Core.Models.Database.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}