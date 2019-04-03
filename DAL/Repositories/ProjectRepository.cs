using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task = DAL.Entities.Task;

namespace DAL.Repositories
{
    public class ProjectRepository : IProjectRepository, IGenericRepository<Project>
    {
        private readonly IContext _context;

        public ProjectRepository(IContext context)
        {
            _context = context;
        }

        public void Create(Project item)
        {
            _context.Projects.Add(item);
        }

        public Project Delete(int id)
        {
            var project = _context.Projects.Find(id);

            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            return project;
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return _context.Projects.Where(predicate);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var p = _context.Projects.ToListAsync();
            return await _context.Projects.ToListAsync();
        }

        public void Update(Project item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public Project GetById(int id)
        {
            return _context.Projects.Find(id);
        }

        public async Task<IEnumerable<Project>> GetAllByUserNameAsync(string userName)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return await _context.Projects.Where(p => p.Team.Contains(user)).ToArrayAsync();
        }
    }
}
