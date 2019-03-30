using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        User Create(UserDTO userDTO);
        User Edit(int id, UserDTO userDTO);
        User Delete(int id);
        UserDTO Get(int id);
        IEnumerable<UserDTO> GetAll();
        IEnumerable<UserDTO> GetByProject(int id);
        UserDTO GetByApplicationUserId(string id);
        User AddProject(int userId, int projectId);
        User RemoveProject(int userId, int projectId);
    }
}
