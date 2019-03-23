using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;

namespace BLL.Services
{
    public sealed class ProjectService : IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Project Create(ProjectDTO projectDTO)
        {
            Project project = new Project()
            {
                Name = projectDTO.Name,
                Tag = projectDTO.Tag,
                Tasks = null,
                Team = null
            };

            _unitOfWork.Projects.Create(project);
            _unitOfWork.Save();

            return project;
        }

        public Project Delete(int id)
        {
            Project project = _unitOfWork.Projects.Delete(id); //????return Project
            _unitOfWork.Save();

            return project;
        }

        public Project Edit(int id, ProjectDTO projectDTO)
        {
            var project = _unitOfWork.Projects.Get(id);

            if (project != null)
            {
                if (projectDTO.Name != null && projectDTO.Name != project.Name)
                    project.Name = projectDTO.Name;
                if (projectDTO.Tag != null && projectDTO.Tag != project.Tag)
                    project.Tag = projectDTO.Tag;

                _unitOfWork.Projects.Update(project);
                _unitOfWork.Save();
            }

            return project;
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            return Mapper.AutoMapperConfig.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(_unitOfWork.Projects.GetAll());
        }

        public ProjectDTO Get(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<Project, ProjectDTO>(_unitOfWork.Projects.Get(id)); ;
        }
    }
}
