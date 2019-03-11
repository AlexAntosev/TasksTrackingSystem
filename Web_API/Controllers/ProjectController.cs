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

        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("api/Project/Create")]
        public void Create(Project project)
        {
            _service.Create(project.Name, project.Tag);
        }

        [HttpPut]
        [Route("api/Project/Update/{id}")]
        public void Update(int id, Project project)
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
