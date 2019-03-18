using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

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
        public IHttpActionResult Get()
        {
            IEnumerable<ProjectDTO> projects = _service.GetAll();
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        [HttpGet]
        [Route("api/Project/Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            ProjectDTO project = _service.Get(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Route("api/Project/Create")]
        public IHttpActionResult Create(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Create(project);

            return Ok();
        }

        [HttpPut]
        [Route("api/Project/Update/{id}")]
        public IHttpActionResult Update(int id, ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Edit(id, project);

            return Ok();
        }

        [HttpDelete]
        [Route("api/Project/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Delete(id);

            return Ok();
        }
    }
}
