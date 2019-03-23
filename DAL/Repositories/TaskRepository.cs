using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>
    /// Processing with database in Tasks table
    /// </summary>
    public class TaskRepository : IGenericRepository<Task>
    {
        private readonly IContext _context;

        /// <summary>
        /// Initialize a new instance of repository
        /// </summary>
        public TaskRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creating new task entity in database
        /// </summary>
        public void Create(Task item)
        {
            _context.Tasks.Add(item);
        }

        /// <summary>
        /// Deleting task entity by id in database
        /// </summary>
        public Task Delete(int id)
        {
            var currentTask = _context.Tasks.Find(id);

            if (currentTask != null)
                _context.Tasks.Remove(currentTask);
            return currentTask;
        }

        /// <summary>
        /// Get task entities by predicate in database
        /// </summary>
        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return _context.Tasks.Where(predicate);
        }

        /// <summary>
        /// Get task entity by id in database
        /// </summary>
        public Task Get(int id)
        {
            return _context.Tasks.Find(id);
        }

        /// <summary>
        /// Get all task entities in database
        /// </summary>
        public IEnumerable<Task> GetAll()
        {
            var tasks = _context.Tasks.OrderBy(t => t.Name);;
            return tasks;
        }

        /// <summary>
        /// Update task entity in database
        /// </summary>
        public bool Update(Task item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
