using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Project Create(string name, string tag)
        {
            Project project = new Project()
            {
                Name = name,
                Tag = tag,
                Tasks = null,
                Team = null
            };

            _unitOfWork.Projects.Create(project);
            _unitOfWork.Save();

            return project;
        }

        public Project Delete(int id)
        {
            var project = _unitOfWork.Projects.Get(id);

            if (project != null)
            {
                _unitOfWork.Projects.Delete(id);
                _unitOfWork.Save();
            }

            return project;
        }

        public Project Edit(int id, string name, string tag)
        {
            var project = _unitOfWork.Projects.Get(id);

            if (project != null)
            {
                if (name != null && name != project.Name)
                    project.Name = name;
                if (tag != null && tag != project.Tag)
                    project.Tag = tag;
            }

            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();

            return project;
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            return Mapper.AutoMapperConfig.Mapper.Map<List<Project>, List<ProjectDTO>>(_unitOfWork.Projects.GetAll().ToList());
        }

        public ProjectDTO Get(int id)
        {
            return Mapper.AutoMapperConfig.Mapper.Map<Project, ProjectDTO>(_unitOfWork.Projects.Get(id));
        }
    }
}
