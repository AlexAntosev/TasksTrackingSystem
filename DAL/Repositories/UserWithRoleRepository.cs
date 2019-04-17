using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserWithRoleRepository : IUserWithRoleRepository
    {
        private readonly IContext _context;

        public UserWithRoleRepository(IContext context)
        {
            _context = context;
        }

        public void Create(UserWithRole item)
        {
            _context.UsersWithRoles.Add(item);
        }

        public UserWithRole Delete(int id)
        {
            UserWithRole userWithRole = _context.UsersWithRoles.Find(id);
            if (userWithRole != null)
            {
                _context.UsersWithRoles.Remove(userWithRole);
            }

            return userWithRole;
        }

        public IEnumerable<UserWithRole> Find(Func<UserWithRole, bool> predicate)
        {
            return _context.UsersWithRoles.Where(predicate);
        }

        public async Task<List<UserWithRole>> GetAllAsync()
        {
            return await _context.UsersWithRoles.ToListAsync();
        }

        public UserWithRole GetById(int id)
        {
            return _context.UsersWithRoles.Find(id);
        }

        public async Task<UserWithRole> GetByIdAsync(int id)
        {
            return await _context.UsersWithRoles.FindAsync(id);
        }

        public void Update(UserWithRole item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<UserWithRole>> GetAllUsersWithRolesByProjectIdAsync(int id)
        {
            return await _context.UsersWithRoles.Include(u => u.User).Where(u => u.Projects.Any(p => p.Id == id)).ToListAsync();
        }

        public async Task<UserWithRole> GetUserWithRoleByUserIdAsync(int userId)
        {
            return await _context.UsersWithRoles.Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        
        public async Task<UserWithRole> GetUserWithRoleByUserIdAndProjectIdAsync(int userId, int projectId)
        {
            return await _context.UsersWithRoles.Where(u => u.UserId == userId).Where(u => u.Projects.Any(p =>p.Id == projectId)).FirstOrDefaultAsync();
        }
    }
}
