using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User AddProject(int userId, ProjectDTO projectDTO)
        {
            User user = _unitOfWork.Users.Get(userId);
            var project = Mapper.AutoMapperConfig.Mapper.Map<ProjectDTO, Project>(projectDTO);
            if (user != null)
            {
                if (!user.Projects.Contains(project))
                {
                    user.Projects.Add(project);

                    _unitOfWork.Users.Update(user);
                    _unitOfWork.Save();
                }
            }

            return user;
        }

        public User Create(UserDTO userDTO)
        {
            User user = new User()
            {
                Projects = null,
                ApplicationUserId = userDTO.ApplicationUserId
            };

            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();

            return user;
        }

        public User Delete(int id)
        {
            User user = _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();

            return user;
        }

        public User Edit(int id, UserDTO userDTO)
        {
            User user = _unitOfWork.Users.Get(id);

            if (user != null)
            {
                user.FirstName = userDTO.FirstName;

                _unitOfWork.Users.Update(user);
                _unitOfWork.Save();
            }

            return user;
        }

        public UserDTO Get(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<User, UserDTO>(_unitOfWork.Users.Get(id));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return Mapper.AutoMapperConfig.Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_unitOfWork.Users.GetAll());
        }

        public UserDTO GetByApplicationUserId(string id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<User, UserDTO>(_unitOfWork.Users.Find(u => u.ApplicationUserId == id).FirstOrDefault());
        }
    }
}
