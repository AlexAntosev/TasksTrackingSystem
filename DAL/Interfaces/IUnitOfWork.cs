using System;
using System.Threading.Tasks;

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
        ITaskRepository Tasks { get; }

        /// <summary>
        /// Get projects repository from database
        /// </summary>
        IProjectRepository Projects { get; }

        /// <summary>
        /// Get users repository from database
        /// </summary>
        IUserRepository Users { get; }
        
        /// <summary>
        /// Get comments repository from database
        /// </summary>
        ICommentRepository Comments { get; }

        /// <summary>
        /// Method for saving data
        /// </summary>
        void Save();

        /// <summary>
        /// Method for async saving data
        /// </summary>
        Task<int> SaveAsync();
    }
}
