using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IGenericRepository<User>
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

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.OrderBy(u => u.Id);
        }

        public bool Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
