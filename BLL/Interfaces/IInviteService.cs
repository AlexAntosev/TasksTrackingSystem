using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInviteService
    {
        System.Threading.Tasks.Task<Invite> CreateInviteAsync(InviteDTO inviteDTO);
        System.Threading.Tasks.Task DeleteInviteAsync(int id);
        Task<Invite> GetInviteByIdAsync(int id);
        Task<List<Invite>> GetAllInvitesByReceiverIdAsync(int userId);
        Task<List<Invite>> GetAllInvitesByProjectIdAsync(int projectId);
    }
}
