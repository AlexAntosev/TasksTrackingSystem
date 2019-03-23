using DAL.Entities;
using DAL.Interfaces;
using System;

namespace DAL.Repositories
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// Get access to use all repositories to work with database
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContext _context;

        private IGenericRepository<Task> _taskRepository;
        private IGenericRepository<Project> _projectRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<UserProfile> _userProfileRepository;
        private IGenericRepository<Comment> _commentRepository;

        /// <summary>
        /// Get task repository
        /// </summary>
        IGenericRepository<Task> IUnitOfWork.Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_context));

        /// <summary>
        /// Get task repository
        /// </summary>
        IGenericRepository<Project> IUnitOfWork.Projects => _projectRepository ?? (_projectRepository = new ProjectRepository(_context));

        /// <summary>
        /// Get user repository
        /// </summary>
        public IGenericRepository<User> Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        /// <summary>
        /// Get user profile repository
        /// </summary>
        public IGenericRepository<UserProfile> UserProfiles =>
            _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_context));

        /// <summary>
        /// Get comment repository
        /// </summary>
        public IGenericRepository<Comment> Comments =>
            _commentRepository ?? (_commentRepository = new CommentRepository(_context));

        public UnitOfWork(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save all changes in database
        /// </summary>
        public void Save()
        {
            _context.Save();
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
