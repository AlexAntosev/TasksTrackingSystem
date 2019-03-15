using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.DTO;

namespace BLL.Services
{
    public class TaskService : ITaskService
    {
        private IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Create(string name, string description, int projectId, int priorityId, DateTime deadline)
        {
            Task task = new Task()
            {
                Id = _unitOfWork.Tasks.Find(t => t.Name == name).FirstOrDefault().Id,
                Name = name,
                Description = description,
                PriorityId = priorityId,
                ProjectId = projectId,
                Deadline = deadline,
                Date = DateTime.Now,
                Comments = null
            };

            _unitOfWork.Tasks.Create(task);
            _unitOfWork.Save();

            return task;
        }

        public Task Delete(int id)
        {
            var task = _unitOfWork.Tasks.Get(id);

            if (task != null)
            {
                _unitOfWork.Tasks.Delete(id);
                _unitOfWork.Save();
            }

            return task;
        }

        public TaskDTO Get(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<Task, TaskDTO>(_unitOfWork.Tasks.Get(id));
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return Mapper.AutoMapperConfig.Mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(_unitOfWork.Tasks.GetAll());
        }

        public IEnumerable<TaskDTO> GetByProject(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(_unitOfWork.Tasks.Find(t => t.ProjectId == id));
        }

        public Task Update(int id, string name, string description, int projectId, int priorityId, DateTime deadline)
        {
            var task = _unitOfWork.Tasks.Get(id);

            if (task != null)
            {
                task.Name = name;
                task.Description = description;
                task.ProjectId = projectId;
                task.PriorityId = priorityId;
                task.Deadline = deadline;

                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
            }

            return task;
        }
    }
}
