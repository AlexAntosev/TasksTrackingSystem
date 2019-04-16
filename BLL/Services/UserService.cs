using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(UserDTO userDTO)
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
            
            //if (await _unitOfWork.Users.GetUserByUserNameAsync(userDTO.UserName) == null) 
            //{
            //    throw new ArgumentException("User with current username is already exist.");
            //}
            
            User user = _mapper.Map<UserDTO, User>(userDTO);

            _unitOfWork.Users.Create(user);
            await _unitOfWork.SaveAsync();

            return user;
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
