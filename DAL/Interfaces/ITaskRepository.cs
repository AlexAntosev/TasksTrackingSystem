using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetAllByProjectIdAsync(int id);
    }
}
