using System.Collections.Generic;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByAuthenticationIdAsync(string id);
        
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
