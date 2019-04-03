using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task CreateAsync(TaskDTO taskDTO, int projectId);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(int id, TaskDTO taskDTO, int projectId);
        System.Threading.Tasks.Task<TaskDTO> GetByIdAsync(int id);
        System.Threading.Tasks.Task<List<TaskDTO>> GetAllAsync();
        System.Threading.Tasks.Task<IEnumerable<TaskDTO>> GetAllByProjectIdAsync(int id);
    }
}
