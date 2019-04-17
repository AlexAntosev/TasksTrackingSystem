using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace BLL.Services
{
    public sealed class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task<Task> CreateTaskAsync(TaskDTO taskDTO)
        {
            if (taskDTO == null)
            {
                throw new ArgumentNullException(nameof(taskDTO));
            }

            if (taskDTO.Name == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            if (await _unitOfWork.Projects.GetByIdAsync(taskDTO.ProjectId) == null)
            {
                throw new ArgumentNullException("Current project is not exist.");
            }


            Task task = _mapper.Map<TaskDTO, Task>(taskDTO);
            task.Comments = new List<Comment>();

            _unitOfWork.Tasks.Create(task);
            await _unitOfWork.SaveAsync();

            return task;
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

        public async System.Threading.Tasks.Task UpdateTaskAsync(int id, TaskDTO taskDTO)
        {
            if (taskDTO == null)
            {
                throw new ArgumentNullException(nameof(taskDTO));
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

            if (task.ProjectId != taskDTO.ProjectId)
                task.ProjectId = taskDTO.ProjectId;

            if (task.CreatorId != taskDTO.CreatorId)
                task.CreatorId = taskDTO.CreatorId;

            if (task.ExecutorId != taskDTO.ExecutorId)
                task.ExecutorId = taskDTO.ExecutorId;

            if (task.Priority != Convert.ToInt32(taskDTO.Priority))
                task.Priority = Convert.ToInt32(taskDTO.Priority);

            if (task.Type != Convert.ToInt32(taskDTO.Type))
                task.Type = Convert.ToInt32(taskDTO.Type);

            if (task.Status != Convert.ToInt32(taskDTO.Status))
                task.Status = Convert.ToInt32(taskDTO.Status);

            if (task.Created.ToString() != taskDTO.Created)
                task.Created = Convert.ToDateTime(taskDTO.Created);

            if (task.Updated.ToString() != taskDTO.Updated)
                task.Updated = Convert.ToDateTime(taskDTO.Updated);

            if (task.Deadline.ToString() != taskDTO.Deadline)
                task.Deadline = Convert.ToDateTime(taskDTO.Deadline);

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveAsync();
        }
    }
}
