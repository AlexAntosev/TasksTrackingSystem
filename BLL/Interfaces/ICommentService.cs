using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CommentDTO commentDTO);
        Task DeleteCommentAsync(int id);
        Task EditCommentASync(int id, CommentDTO commentDTO);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<List<Comment>> GetAllCommentsByTaskIdAsync(int taskId);
    }
}
