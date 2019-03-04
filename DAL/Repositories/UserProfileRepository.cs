﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly CompanyContext _context;

        public UserProfileRepository(CompanyContext context)
        {
            _context = context;
        }

        public void Create(UserProfile item)
        {
            _context.UserProfiles.Add(item);
        }

        public UserProfile Delete(int id)
        {
            var userProfile = _context.UserProfiles.Find(id);

            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
            }

            return userProfile;
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _context.UserProfiles.Where(predicate);
        }

        public UserProfile Get(int id)
        {
            return _context.UserProfiles.Find(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _context.UserProfiles.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
        }

        public bool Update(UserProfile item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
