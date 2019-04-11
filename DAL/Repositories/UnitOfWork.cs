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
        
        public ITaskRepository Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_context));
        
        public IProjectRepository Projects => _projectRepository ?? (_projectRepository = new ProjectRepository(_context));
        
        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));
        
        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_context));
        
        public void Save()
        {
            _context.Save();
        }
        
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
