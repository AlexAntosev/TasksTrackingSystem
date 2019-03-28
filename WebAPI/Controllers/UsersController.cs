using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private IUserService _userService;
        private IProjectService _projectService;

        public UsersController(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            IEnumerable<UserDTO> users = _userService.GetAll();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            UserDTO user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            UserDTO currentUser = _userService.Get(id);
            if (currentUser == null)
            {
                return NotFound();
            }
            _userService.Edit(id, user);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            UserDTO currentUser = _userService.Get(id);
            if (currentUser == null)
            {
                return NotFound();
            }
            _userService.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpPut]
        [Route("api/Projects/{projectId}/Users")]
        public IHttpActionResult AddToProject(int projectId, int userId)
        {
            _userService.AddProject(userId, projectId);

            return Ok();
        }

        [HttpDelete]
        [Route("api/Projects/{projectId}/Users")]
        public IHttpActionResult RemoveFromProject(int projectId, int userId)
        {
            UserDTO currentUser = _userService.Get(userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            ProjectDTO currentProject = _projectService.Get(projectId);
            if (currentProject == null)
            {
                return NotFound();
            }
            _userService.RemoveProject(userId, projectId);

            return NotFound();
        }
    }
}
