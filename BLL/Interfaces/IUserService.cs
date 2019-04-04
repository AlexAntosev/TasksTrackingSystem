using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDTO userDTO);
        Task EditUserAsync(int id, UserDTO userDTO);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersByProjectIdAsync(int id);
        Task<User> GetUserByAuthenticationIdAsync(string id);
        Task AddProjectToUserAsync(int userId, int projectId);
        Task RemoveProjectFromUserAsync(int userId, int projectId);
    }
}
