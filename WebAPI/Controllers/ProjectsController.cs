using BLL.DTO;
using BLL.Interfaces;
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
        public async Task<IHttpActionResult> GetProjectsAsync()
        {
            List<ProjectDTO> projects =  await _service.GetAllAsync();
            if (projects == null)
            {
                return NotFound();
            }
            
            return Ok(projects);
        }

        [HttpGet]
        [Route("api/Users/{userName}/Projects")]
        public async Task<IHttpActionResult> GetProjectsByUserNameAsync(string userName)
        {
            IEnumerable<ProjectDTO> projects = await _service.GetAllByUserNameAsync(userName);
            if (projects == null)
            {
                return NotFound();
            }
            
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProjectByIdAsync(int id)
        {
            ProjectDTO project = await _service.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProjectAsync(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            await _service.CreateAsync(project);

            return Ok(project); // Created();
        }

        [Authorize]
        [HttpPut]
        public async Task<IHttpActionResult> EditProjectAsync(int id, ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            ProjectDTO currentProject = await _service.GetByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            await _service.EditAsync(id, project);

            return Ok(project);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProjectAsync(int id)
        {
            ProjectDTO currentProject = await _service.GetByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
