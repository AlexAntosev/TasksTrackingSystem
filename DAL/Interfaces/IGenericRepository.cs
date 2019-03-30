using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for database repository
    /// </summary>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Method for getting all data from table
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Method for getting data item from table by id 
        /// </summary>
        T Get(int id);

        /// <summary>
        /// Method for creating a new item in data table
        /// </summary>
        void Create(T item);

        /// <summary>
        /// Method for updating item in data table
        /// </summary>
        bool Update(T item);

        /// <summary>
        /// Method for deleting an item from data table by id
        /// </summary>
        T Delete(int id);

        /// <summary>
        /// Method for finding a collection of item by current predicate
        /// </summary>
        IEnumerable<T> Find(Func<T, bool> predicate);

        Task<List<Project>> GetAllAsync();
    }
}
