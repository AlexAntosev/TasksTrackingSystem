using DAL.Entities;
using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        Task Create(string name, string description, int projectId, int priorityId, DateTime deadline);
        Task Delete(int id);
        Task Update(int id, string name, string description, int projectId, int priorityId, DateTime deadline);
        TaskDTO Get(int id);
        IEnumerable<TaskDTO> GetAll();
        IEnumerable<TaskDTO> GetByProject(int id);
    }
}
