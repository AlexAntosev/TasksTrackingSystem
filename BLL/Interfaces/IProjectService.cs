using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task CreateAsync(ProjectDTO projectDTO);
        Task DeleteAsync(int id);
        Task EditAsync(int id, ProjectDTO projectDTO);
        Task<ProjectDTO> GetByIdAsync(int id);
        Task<IEnumerable<ProjectDTO>> GetAllByUserNameAsync(string userName);
        Task<List<ProjectDTO>> GetAllAsync();
    }
}
