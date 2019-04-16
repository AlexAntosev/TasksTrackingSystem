using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(ProjectDTO projectDTO);
        Task DeleteProjectAsync(int id);
        Task EditProjectAsync(int id, ProjectDTO projectDTO);
        Task<Project> GetProjectByIdAsync(int id);
        Task<List<Project>> GetAllProjectsByUserNameAsync(string userName);
        Task<List<Project>> GetAllProjectsAsync();
    }
}
