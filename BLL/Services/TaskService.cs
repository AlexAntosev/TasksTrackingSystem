using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Linq;

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
    }
}
