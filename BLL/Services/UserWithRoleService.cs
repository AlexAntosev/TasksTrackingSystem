using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserWithRoleService : IUserWithRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserWithRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserWithRole> CreateUserWithRoleAsync(UserWithRoleDTO userWithRoleDTO, int projectId)
        {
            if (userWithRoleDTO == null)
            {
                throw new ArgumentNullException(nameof(userWithRoleDTO));
            }

            UserWithRole userWithRole = _mapper.Map<UserWithRoleDTO, UserWithRole>(userWithRoleDTO);

            var project = _unitOfWork.Projects.GetById(projectId);
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            
            userWithRole.Projects.Add(project);
            _unitOfWork.UsersWithRoles.Create(userWithRole);
            
            await _unitOfWork.SaveAsync();

            return userWithRole;
        }

        public async System.Threading.Tasks.Task DeleteUserWithRoleAsync(int id)
        {
            _unitOfWork.UsersWithRoles.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async System.Threading.Tasks.Task EditUserWithRoleASync(int id, UserWithRoleDTO userWithRoleDTO)
        {
            if (userWithRoleDTO == null)
            {
                throw new ArgumentNullException(nameof(userWithRoleDTO));
            }

            var userWithRole = _unitOfWork.UsersWithRoles.GetById(id);
            if (userWithRole == null)
            {
                throw new ArgumentNullException("Comment is not exist.");
            }

            if (userWithRole.Role != userWithRole.Role)
                userWithRole.Role = userWithRole.Role;

            _unitOfWork.UsersWithRoles.Update(userWithRole);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<UserWithRole>> GetAllUsersWithRolesAsync()
        {
            return await _unitOfWork.UsersWithRoles.GetAllAsync();
        }

        public async Task<List<UserWithRole>> GetAllUsersWithRolesByProjectIdAsync(int projectId)
        {
            return await _unitOfWork.UsersWithRoles.GetAllUsersWithRolesByProjectIdAsync(projectId);
        }

        public async Task<UserWithRole> GetUserWithRoleByIdAsync(int id)
        {
            return await _unitOfWork.UsersWithRoles.GetByIdAsync(id);
        }

        public async Task<UserWithRole> GetUserWithRoleByUserIdAsync(int userId)
        {
            return await _unitOfWork.UsersWithRoles.GetUserWithRoleByUserIdAsync(userId);
        }
    }
}
