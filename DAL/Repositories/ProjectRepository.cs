using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
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

        public Project Get(int id)
        {
            var project = _context.Projects.Find(id);
            return project;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.OrderBy(p => p.Name);
        }

        public bool Update(Project item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
