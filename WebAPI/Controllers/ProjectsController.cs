using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public sealed class ProjectsController : ApiController
    {
        private IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetProjects()
        {
            IEnumerable<ProjectDTO> projects =  _service.GetAll();
            if (projects == null)
            {
                return NotFound();
            }
            
            return Ok(projects);
        }

        [HttpGet]
        [Route("api/Users/{userName}/Projects")]
        public IHttpActionResult GetProjectsByUserName(string userName)
        {
            IEnumerable<ProjectDTO> projects = _service.GetByUserName(userName);
            if (projects == null)
            {
                return NotFound();
            }
            
            return Ok(projects);
        }

        [HttpGet]
        public IHttpActionResult GetProjectById(int id)
        {
            ProjectDTO project = _service.Get(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IHttpActionResult CreateProject(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            _service.Create(project);

            return Ok(project); // Created();
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult EditProject(int id, ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            ProjectDTO currentProject = _service.Get(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            _service.Edit(id, project);

            return Ok(project);
        }

        [HttpDelete]
        public IHttpActionResult DeleteProject(int id)
        {
            ProjectDTO currentProject = _service.Get(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            _service.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
