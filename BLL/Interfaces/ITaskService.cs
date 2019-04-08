using BLL.DTO;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task CreateTaskAsync(TaskDTO taskDTO, int projectId, int creatorUserId, int executorUserId);
        System.Threading.Tasks.Task DeleteTaskAsync(int id);
        System.Threading.Tasks.Task UpdateTaskAsync(int id, TaskDTO taskDTO, int projectId, int creatorUserId, int executorUserId);
        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int id);
        System.Threading.Tasks.Task<List<Task>> GetAllTasksAsync();
        System.Threading.Tasks.Task<List<Task>> GetAllTasksByProjectIdAsync(int id);
    }
}
