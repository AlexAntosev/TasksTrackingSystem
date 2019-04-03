using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public sealed class ProjectService : IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO));
            }

            if (projectDTO.Name == null || projectDTO.Tag == null)
            {
                throw new ArgumentException("Some fields are empty.");
            }

            Project project  = Mapper.AutoMapperConfig.Mapper.Map<ProjectDTO, Project>(projectDTO);

            _unitOfWork.Projects.Create(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Projects.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(int id, ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                throw new ArgumentNullException(nameof(projectDTO));
            }

            if (projectDTO.Name == null || projectDTO.Tasks == null)
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

            if (project.Name != projectDTO.Name)
                project.Tag = projectDTO.Tag;

            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllByUserNameAsync(string userName)
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<IEnumerable<Project>>, Task<IEnumerable<ProjectDTO>>>(
                _unitOfWork.Projects.GetAllByUserNameAsync(userName));
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            return await Mapper.AutoMapperConfig.Mapper.Map<Task<List<Project>>, Task<List<ProjectDTO>>>(
                _unitOfWork.Projects.GetAllAsync());
        }

        public Task<ProjectDTO> GetByIdAsync(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<Task<Project>, Task<ProjectDTO>>(
                _unitOfWork.Projects.GetByIdAsync(id));
        }
    }
}
