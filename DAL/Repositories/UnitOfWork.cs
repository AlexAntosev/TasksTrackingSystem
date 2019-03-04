using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// Get access to use all repositories to work with database
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyContext _context;

        private IRepository<Task> _taskRepository;
        private IRepository<Project> _projectRepository;
        private IRepository<User> _userRepository;
        private IRepository<UserProfile> _userProfileRepository;
        private IRepository<Comment> _commentRepository;

        /// <summary>
        /// Get task repository
        /// </summary>
        IRepository<Task> IUnitOfWork.Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_context));

        /// <summary>
        /// Get task repository
        /// </summary>
        IRepository<Project> IUnitOfWork.Projects => _projectRepository ?? (_projectRepository = new ProjectRepository(_context));

        /// <summary>
        /// Get user repository
        /// </summary>
        public IRepository<User> Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        /// <summary>
        /// Get user profile repository
        /// </summary>
        public IRepository<UserProfile> UserProfiles =>
            _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_context));

        /// <summary>
        /// Get comment repository
        /// </summary>
        public IRepository<Comment> Comments =>
            _commentRepository ?? (_commentRepository = new CommentRepository(_context));

        public UnitOfWork(CompanyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save all changes in database
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
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

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
