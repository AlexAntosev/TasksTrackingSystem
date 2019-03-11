using DAL.Entities;
using System;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        Task Create(string name, string description, int projectId, int priorityId, DateTime deadline);
        Task Delete(int id);
    }
}
