using System.Collections.Generic;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByAuthenticationIdAsync(string id);

        Task<IEnumerable<User>> GetByProjectIdAsync(int id);

        Task<User> GetByUserNameAsync(string userName);
    }
}
