using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository, IGenericRepository<User>
    {
        private readonly IContext _context;

        public UserRepository(IContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public User Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
            }

            return user;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate);
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByAuthenticationIdAsync(string id)
        {
            return await _context.Users.Where(u => u.ApplicationUserId == id).FirstOrDefaultAsync();
        }
        
        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<List<User>> GetAllUsersByProjectIdAsync(int id)
        {
            var project = await _context.Projects.Where(p => p.Id == id).FirstOrDefaultAsync();
            return await _context.Users.Where(u => u.Projects.Contains(project)).ToListAsync();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
