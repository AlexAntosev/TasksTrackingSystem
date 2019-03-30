using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Project Create(ProjectDTO projectDTO);
        Project Delete(int id);
        Project Edit(int id, ProjectDTO projectDTO);
        ProjectDTO Get(int id);
        IEnumerable<ProjectDTO> GetAll();
        Task<List<Project>> GetAllAsync();
    }
}
