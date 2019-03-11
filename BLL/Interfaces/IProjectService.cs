using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Project Create(string name, string tag);
        Project Delete(int id);
        Project Edit(int id, string name, string tag);
        ProjectDTO Get(int id);
        IEnumerable<ProjectDTO> GetAll();
    }
}
