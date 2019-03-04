using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(ProjectDTO project)
        {
            Project _project = new Project()
            {
                Name = project.Name,
                Tag = project.Tag,
                Tasks = null,
                Team = null
            };

            _unitOfWork.Projects.Create(_project);
            _unitOfWork.Save();
        }
    }
}
