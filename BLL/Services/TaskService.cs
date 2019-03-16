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

        public Task Create(TaskDTO taskDTO, int projectId)
        {
            Task task = new Task()
            {
                ///Id = _unitOfWork.Tasks.Find(t => t.Name == taskDTO.Name).FirstOrDefault().Id,
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                PriorityId = Convert.ToInt32(taskDTO.Priority),
                ProjectId = projectId,
                Deadline = DateTime.Now,
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

        public Task Update(int id, TaskDTO taskDTO, int projectId)
        {
            var task = _unitOfWork.Tasks.Get(id);

            if (task != null)
            {
                task.Name = taskDTO.Name;
                task.Description = taskDTO.Description;
                task.ProjectId = projectId;
                task.PriorityId = Convert.ToInt32(taskDTO.Priority);
                task.Deadline = taskDTO.Deadline;

                _unitOfWork.Tasks.Update(task);
                _unitOfWork.Save();
            }

            return task;
        }
    }
}
