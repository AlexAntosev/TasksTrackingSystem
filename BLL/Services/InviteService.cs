using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using AutoMapper;

namespace BLL.Services
{
    public sealed class InviteService : IInviteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InviteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async System.Threading.Tasks.Task<Invite> CreateInviteAsync(InviteDTO inviteDTO)
        {
            if (inviteDTO == null)
            {
                throw new ArgumentNullException(nameof(inviteDTO));
            }

            Invite invite = _mapper.Map<InviteDTO, Invite>(inviteDTO);

            _unitOfWork.Invites.Create(invite);
            await _unitOfWork.SaveAsync();

            return invite;
        }

        public async System.Threading.Tasks.Task DeleteInviteAsync(int id)
        {
            _unitOfWork.Invites.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Invite>> GetAllInvitesByProjectIdAsync(int projectId)
        {
            return await _unitOfWork.Invites.GetAllByProjectIdAsync(projectId);
        }

        public async Task<List<Invite>> GetAllInvitesByReceiverIdAsync(int userId)
        {
            return await _unitOfWork.Invites.GetAllByReceiverIdAsync(userId);
        }

        public async Task<Invite> GetInviteByIdAsync(int id)
        {
            return await _unitOfWork.Invites.GetByIdAsync(id);
        }
    }
}
