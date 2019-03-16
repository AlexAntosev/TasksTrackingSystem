using DAL.Entities;
using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        Task Create(TaskDTO taskDTO, int projectId);
        Task Delete(int id);
        Task Update(int id, TaskDTO taskDTO, int projectId);
        TaskDTO Get(int id);
        IEnumerable<TaskDTO> GetAll();
        IEnumerable<TaskDTO> GetByProject(int id);
    }
}
