using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class ProjectController : ApiController
    {
        private IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/Project/Get")]
        public IEnumerable<ProjectDTO> Get()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("api/Project/Get/{id}")]
        public ProjectDTO Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPost]
        [Route("api/Project/Create")]
        public void Create(ProjectDTO project)
        {
            _service.Create(project.Name, project.Tag);
        }

        [HttpPut]
        [Route("api/Project/Update/{id}")]
        public void Update(int id, ProjectDTO project)
        {
            _service.Edit(id, project.Name, project.Tag);
        }

        [HttpDelete]
        [Route("api/Project/Delete/{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
