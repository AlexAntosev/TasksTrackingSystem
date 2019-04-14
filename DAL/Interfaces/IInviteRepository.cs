using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IInviteRepository : IGenericRepository<Invite>
    {
        Task<List<Invite>> GetAllByReceiverIdAsync(int id);
        Task<List<Invite>> GetAllByProjectIdAsync(int id);
    }
}
