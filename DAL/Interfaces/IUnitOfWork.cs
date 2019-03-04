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
        IRepository<Task> Tasks { get; }

        /// <summary>
        /// Get projects repository from database
        /// </summary>
        IRepository<Project> Projects { get; }

        /// <summary>
        /// Get users repository from database
        /// </summary>
        IRepository<User> Users { get; }

        /// <summary>
        /// Get users profiles repository from database
        /// </summary>
        IRepository<UserProfile> UserProfiles { get; }

        /// <summary>
        /// Get comments repository from database
        /// </summary>
        IRepository<Comment> Comments { get; }

        /// <summary>
        /// Method for saving data
        /// </summary>
        void Save();
    }
}
