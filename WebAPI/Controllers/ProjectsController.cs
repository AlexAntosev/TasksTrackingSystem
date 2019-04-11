using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using DAL.Entities;
using BLL.Infrastructure;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace WebAPI.Controllers
{
    [Authorize]
    public sealed class ProjectsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUserService _userService;
        private IProjectService _projectService;

        public ProjectsController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProjectsAsync()
        {
            List<Project> projects =  await _projectService.GetAllProjectsAsync();

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
            List<Project> projects = await _projectService.GetAllProjectsByUserNameAsync(userName);
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
            Project project = await _projectService.GetProjectByIdAsync(id);
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

            AuthenticationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            string currentUserId = user?.Id;

            if (currentUserId == null)
            {
                return Unauthorized();
            }

            User currentAppUser = await _userService.GetUserByAuthenticationIdAsync(currentUserId);

            if (currentAppUser == null)
            {
                return NotFound();
            }

            project.LeadId = currentAppUser.Id;
            await _projectService.CreateProjectAsync(project);
            
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

            Project currentProject = await _projectService.GetProjectByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }
            
            await _projectService.EditProjectAsync(id, project);

            return Ok(project);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProjectAsync(int id)
        {
            Project currentProject = await _projectService.GetProjectByIdAsync(id);
            if (currentProject == null)
            {
                return NotFound();
            }

            if (currentProject.Tasks.Count != 0 || currentProject.Team.Count != 0)
            {
                return BadRequest("Project contains open tasks or users in team");
            }

            await _projectService.DeleteProjectAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
