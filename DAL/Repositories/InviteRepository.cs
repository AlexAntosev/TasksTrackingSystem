using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public sealed class InviteRepository : IInviteRepository
    {
        private readonly IContext _context;

        public InviteRepository(IContext context)
        {
            _context = context;
        }

        public void Create(Invite item)
        {
            _context.Invites.Add(item);
        }

        public Invite Delete(int id)
        {
            var invite = _context.Invites.Find(id);
            if (invite != null)
            {
                _context.Invites.Remove(invite);
            }

            return invite;
        }

        public IEnumerable<Invite> Find(Func<Invite, bool> predicate)
        {
            return _context.Invites.Where(predicate);
        }

        public async Task<List<Invite>> GetAllAsync()
        {
            return await _context.Invites.ToListAsync();
        }

        public async Task<List<Invite>> GetAllByProjectIdAsync(int id)
        {
            return await _context.Invites.Where(i => i.ProjectId == id).ToListAsync();
        }

        public async Task<List<Invite>> GetAllByReceiverIdAsync(int id)
        {
            return await _context.Invites.Where(i => i.ReceiverId == id).ToListAsync();
        }

        public Invite GetById(int id)
        {
            return _context.Invites.Find(id);
        }

        public async Task<Invite> GetByIdAsync(int id)
        {
            return await _context.Invites.FindAsync(id);
        }

        public void Update(Invite item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
