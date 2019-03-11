using System;
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
    public class CommentRepository : IRepository<Comment>
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

        public Comment Get(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.OrderBy(c => c.Time);
        }

        public bool Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }
    }
}
