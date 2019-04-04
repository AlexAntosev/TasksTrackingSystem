using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using DAL.Entities;

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
            List<Project> projects =  await _service.GetAllProjectsAsync();

            if (projects == null)
            {
                return NotFound();
            }


            List<ProjectDTO> projectsDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Project>, List<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        [HttpGet]
        [Route("api/Users/{userName}/Projects")]
        public async Task<IHttpActionResult> GetProjectsByUserNameAsync(string userName)
        {
            List<Project> projects = await _service.GetAllProjectsByUserNameAsync(userName);
            if (projects == null)
            {
                return NotFound();
            }

            List<ProjectDTO> projectsDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Project>, List<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProjectByIdAsync(int id)
        {
            Project project = await _service.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            
            ProjectDTO projectDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<Project, ProjectDTO>(project);
            return Ok(projectDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProjectAsync(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            await _service.CreateProjectAsync(project);

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

            Project currentProject = await _service.GetProjectByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            
            await _service.EditProjectAsync(id, project);

            return Ok(project);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProjectAsync(int id)
        {
            Project currentProject = await _service.GetProjectByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            await _service.DeleteProjectAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
