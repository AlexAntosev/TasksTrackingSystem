using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        T GetById(int id);

        void Create(T item);

        void Update(T item);

        T Delete(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
