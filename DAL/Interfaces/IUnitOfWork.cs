using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for getting database by repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        
        IProjectRepository Projects { get; }
        
        IUserRepository Users { get; }
        
        ICommentRepository Comments { get; }

        IInviteRepository Invites { get; }
        
        void Save();
        
        Task<int> SaveAsync();
    }
}
