using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class TaskRepository : ITaskRepository, IGenericRepository<Task>
    {
        private readonly IContext _context;
        
        public TaskRepository(IContext context)
        {
            _context = context;
        }
        
        public void Create(Task item)
        {
            _context.Tasks.Add(item);
        }
        
        public Task Delete(int id)
        {
            var currentTask = _context.Tasks.Find(id);

            if (currentTask != null)
                _context.Tasks.Remove(currentTask);
            return currentTask;
        }
        
        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return _context.Tasks.Where(predicate);
        }
        
        public async System.Threading.Tasks.Task<Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        
        public async System.Threading.Tasks.Task<List<Task>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }
        
        public void Update(Task item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public Task GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksByProjectIdAsync(int id)
        {
            return await _context.Tasks.Where(t => t.ProjectId == id).ToListAsync();
        }
    }
}
