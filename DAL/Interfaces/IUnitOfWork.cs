using DAL.Entities;
using System;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for getting database by repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get tasks repository from database
        /// </summary>
        IGenericRepository<Task> Tasks { get; }

        /// <summary>
        /// Get projects repository from database
        /// </summary>
        IGenericRepository<Project> Projects { get; }

        /// <summary>
        /// Get users repository from database
        /// </summary>
        IGenericRepository<User> Users { get; }
        
        /// <summary>
        /// Get comments repository from database
        /// </summary>
        IGenericRepository<Comment> Comments { get; }

        /// <summary>
        /// Method for saving data
        /// </summary>
        void Save();
    }
}
