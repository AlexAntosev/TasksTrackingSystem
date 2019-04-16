using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDTO userDTO);
        Task EditUserAsync(int id, UserDTO userDTO);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByAuthenticationIdAsync(string id);
    }
}
