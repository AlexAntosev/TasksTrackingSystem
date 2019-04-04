using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProjectToUserAsync(int userId, int projectId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("User is not exist.");
            }

            var project = _unitOfWork.Projects.GetById(projectId);
            if (project == null)
            {
                throw new ArgumentException("Project is not exist.");
            }

            if (user.Projects.Contains(project))
            {
                throw new ArgumentException("Current project is already exist in user project list.");
            }

            user.Projects.Add(project);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
            
        }

        public async Task RemoveProjectFromUserAsync(int userId, int projectId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("User is not exist.");
            }

            var project = _unitOfWork.Projects.GetById(projectId);
            if (project == null)
            {
                throw new ArgumentException("Project is not exist.");
            }

            if (!user.Projects.Contains(project))
            {
                throw new ArgumentException("Current project is not exist in user project list.");
            }

            user.Projects.Remove(project);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            if (userDTO.UserName == null || userDTO.FirstName == null || userDTO.LastName == null ||
                userDTO.Position == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }
            
            if (_unitOfWork.Users.GetUserByUserNameAsync(userDTO.UserName) != null) 
            {
                throw new ArgumentException("User with current username is already exist.");
            }

            User user = Mapper.AutoMapperConfig.Mapper.Map<UserDTO, User>(userDTO);

            _unitOfWork.Users.Create(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            User user = _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditUserAsync(int id, UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ArgumentNullException(nameof(userDTO));
            }

            if (userDTO.UserName == null || userDTO.FirstName == null || userDTO.LastName == null ||
                userDTO.Position == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                throw new ArgumentNullException("User is not exist.");
            }

            if (user.FirstName != userDTO.FirstName)
                user.FirstName = userDTO.FirstName;

            if (user.LastName != userDTO.LastName)
                user.LastName = userDTO.LastName;

            if (user.UserName != userDTO.UserName)
                user.UserName = userDTO.UserName;

            if (user.Position != userDTO.Position)
                user.Position = userDTO.Position;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<List<User>> GetAllUsersByProjectIdAsync(int id)
        {
            return await _unitOfWork.Users.GetAllUsersByProjectIdAsync(id);
        }

        public async Task<User> GetUserByAuthenticationIdAsync(string id)
        {
            return await _unitOfWork.Users.GetUserByAuthenticationIdAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _unitOfWork.Users.GetUserByUserNameAsync(userName);
        }
    }
}
