using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserWithRoleRepository : IGenericRepository<UserWithRole>
    {
        Task<List<UserWithRole>> GetAllUsersWithRolesByProjectIdAsync(int id);
        Task<UserWithRole> GetUserWithRoleByUserIdAsync(int userId);
        Task<UserWithRole> GetUserWithRoleByUserIdAndProjectIdAsync(int userId, int projectId);
    }
}
