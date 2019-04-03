using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserDTO userDTO);
        Task EditAsync(int id, UserDTO userDTO);
        Task DeleteAsync(int id);
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> GetByUserNameAsync(string userName);
        Task<List<UserDTO>> GetAllAsync();
        Task<IEnumerable<UserDTO>> GetByProjectIdAsync(int id);
        Task<UserDTO> GetByAuthenticationIdAsync(string id);
        Task AddProjectAsync(int userId, int projectId);
        Task RemoveProjectAsync(int userId, int projectId);
    }
}
