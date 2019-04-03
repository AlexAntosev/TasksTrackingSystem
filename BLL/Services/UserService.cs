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

        public async Task AddProjectAsync(int userId, int projectId)
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

        public async Task RemoveProjectAsync(int userId, int projectId)
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

        public async Task CreateAsync(UserDTO userDTO)
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
            
            if (_unitOfWork.Users.GetByUserNameAsync(userDTO.UserName) != null) 
            {
                throw new ArgumentException("User with current username is already exist.");
            }

            User user = Mapper.AutoMapperConfig.Mapper.Map<UserDTO, User>(userDTO);

            _unitOfWork.Users.Create(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User user = _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(int id, UserDTO userDTO)
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

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<User>, Task<UserDTO>>(_unitOfWork.Users.GetByIdAsync(id));
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<List<User>>, Task<List<UserDTO>>>(_unitOfWork.Users.GetAllAsync());
        }

        public async Task<IEnumerable<UserDTO>> GetByProjectIdAsync(int id)
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<IEnumerable<User>>, Task<IEnumerable<UserDTO>>>(_unitOfWork.Users.GetByProjectIdAsync(id));
        }

        public async Task<UserDTO> GetByAuthenticationIdAsync(string id)
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<User>, Task<UserDTO>>(_unitOfWork.Users.GetByAuthenticationIdAsync(id));
        }

        public Task<UserDTO> GetByUserNameAsync(string userName)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<Task<User>, Task<UserDTO>>(_unitOfWork.Users.GetByUserNameAsync(userName));
        }
    }
}
