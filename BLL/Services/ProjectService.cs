using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public sealed class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProjectAsync(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO));
            }

            if (projectDTO.Name == null || projectDTO.Tag == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            Project project  = _mapper.Map<ProjectDTO, Project>(projectDTO);

            _unitOfWork.Projects.Create(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            _unitOfWork.Projects.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditProjectAsync(int id, ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO));
            }

            if (projectDTO.Name == null || projectDTO.Tag == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            var project = _unitOfWork.Projects.GetById(id);
            if (project == null)
            {
                throw new ArgumentNullException("Project is not exist.");
            }

            if (project.Name != projectDTO.Name)
                project.Name = projectDTO.Name;

            if (project.Tag != projectDTO.Tag)
                project.Tag = projectDTO.Tag;

            if (project.Url != projectDTO.Url)
                project.Url = projectDTO.Url;

            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Project>> GetAllProjectsByUserNameAsync(string userName)
        {
            return await _unitOfWork.Projects.GetAllProjectsByUserNameAsync(userName);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _unitOfWork.Projects.GetAllAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _unitOfWork.Projects.GetByIdAsync(id);
        }
    }
}
