using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public sealed class TaskService : ITaskService
    {
        private IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(TaskDTO taskDTO, int projectId, int creatorUserId, int executorUserId)
        {
            if (taskDTO == null)
            {
                throw new ArgumentNullException(nameof(taskDTO));
            }

            if (taskDTO.Name == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            if (await _unitOfWork.Projects.GetByIdAsync(projectId) == null)
            {
                throw new ArgumentNullException("Current project is not exist.");
            }

            Task task = new Task()
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                Priority = Convert.ToInt32(taskDTO.Priority),
                ProjectId = projectId,
                CreatorId = creatorUserId,
                ExecutorId = executorUserId,
                Deadline = DateTime.Now,//taskDTO.Deadline,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Comments = new List<Comment>()
            };

            _unitOfWork.Tasks.Create(task);
            await _unitOfWork.SaveAsync();
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            Task task = _unitOfWork.Tasks.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(int id)
        {
            return await _unitOfWork.Tasks.GetByIdAsync(id);
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksAsync()
        {
            return await _unitOfWork.Tasks.GetAllAsync();
        }

        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksByProjectIdAsync(int id)
        {
            return await _unitOfWork.Tasks.GetAllTasksByProjectIdAsync(id);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(int id, TaskDTO taskDTO, int projectId, int creatorUserId, int executorUserId)
        {
            if (taskDTO == null)
            {
                throw new ArgumentNullException(nameof(taskDTO));
            }

            if (_unitOfWork.Projects.GetByIdAsync(projectId) == null)
            {
                throw new ArgumentNullException("Current project is not exist.");
            }

            var task = _unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                throw new ArgumentNullException("Task is not exist.");
            }

            if (task.Name != taskDTO.Name)
                task.Name = taskDTO.Name;

            if (task.Description != taskDTO.Description)
                task.Description = taskDTO.Description;

            if (task.ProjectId != projectId)
                task.ProjectId = projectId;

            if (task.CreatorId != creatorUserId)
                task.CreatorId = creatorUserId;

            if (task.ExecutorId != executorUserId)
                task.ExecutorId = executorUserId;

            if (task.Priority != Convert.ToInt32(taskDTO.Priority))
                task.Priority = Convert.ToInt32(taskDTO.Priority);

            if (task.Created != taskDTO.Created)
                task.Created = taskDTO.Created;

            if (task.Deadline != taskDTO.Deadline)
                task.Deadline = taskDTO.Deadline;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveAsync();
        }
    }
}
