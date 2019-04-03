using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// Get access to use all repositories to work with database
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IContext _context;

        private ITaskRepository _taskRepository;
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get task repository
        /// </summary>
        public ITaskRepository Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_context));

        /// <summary>
        /// Get task repository
        /// </summary>
        public IProjectRepository Projects => _projectRepository ?? (_projectRepository = new ProjectRepository(_context));

        /// <summary>
        /// Get user repository
        /// </summary>
        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        /// <summary>
        /// Get comment repository
        /// </summary>
        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_context));
        
        /// <summary>
        /// Save all changes in database
        /// </summary>
        public void Save()
        {
            _context.Save();
        }

        /// <summary>
        /// Save all changes in database
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await _context.SaveAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
