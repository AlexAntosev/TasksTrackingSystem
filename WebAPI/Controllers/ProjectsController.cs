﻿using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Authorize]
    public sealed class ProjectsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IUserService userService, IMapper mapper)
        {
            _projectService = projectService;
            _userService = userService;
            _mapper = mapper;
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


            List<ProjectDTO> projectsDTO = _mapper.Map<List<Project>, List<ProjectDTO>>(projects);
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

            List<ProjectDTO> projectsDTO = _mapper.Map<List<Project>, List<ProjectDTO>>(projects);
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
            
            ProjectDTO projectDTO = _mapper.Map<Project, ProjectDTO>(project);
            return Ok(projectDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProjectAsync(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The project is not created. The project creating model is incorrectly filled.");
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
            Project createdProject = await _projectService.CreateProjectAsync(project);
            ProjectDTO createdProjectDTO = _mapper.Map<Project, ProjectDTO>(createdProject);
            return Created(Url.Request.RequestUri, createdProjectDTO);
        }

        [Authorize]
        [HttpPut]
        public async Task<IHttpActionResult> EditProjectAsync(int id, ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The project is not edited. The project editing model is incorrectly filled.");
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
