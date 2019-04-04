using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        System.Threading.Tasks.Task<List<Task>> GetAllTasksByProjectIdAsync(int id);
    }
}
