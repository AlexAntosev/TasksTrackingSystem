using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetAllProjectsByUserNameAsync(string userName);
    }
}
