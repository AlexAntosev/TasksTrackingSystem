using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository, IGenericRepository<Comment>
    {
        private readonly IContext _context;

        public CommentRepository(IContext context)
        {
            _context = context;
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        public Comment Delete(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            return comment;
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return _context.Comments.Where(predicate);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public void Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }
    }
}
