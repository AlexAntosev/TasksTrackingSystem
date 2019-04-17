using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IUserWithRoleService
    {
        Task<UserWithRole> CreateUserWithRoleAsync(UserWithRoleDTO userWithRoleDTO, int projectId);
        Task DeleteUserWithRoleAsync(int id);
        Task EditUserWithRoleASync(int id, UserWithRoleDTO userWithRoleDTO);
        Task<UserWithRole> GetUserWithRoleByIdAsync(int id);
        Task<UserWithRole> GetUserWithRoleByUserIdAsync(int userId);
        Task<UserWithRole> GetUserWithRoleByUserIdAndProjectIdAsync(int userId, int projectId);
        Task<List<UserWithRole>> GetAllUsersWithRolesAsync();
        Task<List<UserWithRole>> GetAllUsersWithRolesByProjectIdAsync(int projectId);
    }
}
