using System.Collections.Generic;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllCommentsByTaskIdAsync(int taskId);
    }
}
