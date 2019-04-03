using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> GetUsersByProjectIdAsync(int projectId)
        {
            IEnumerable<UserDTO> users = await _userService.GetByProjectIdAsync(projectId);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsersAsync()
        {
            IEnumerable<UserDTO> users = await _userService.GetAllAsync();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("api/Users/{userName}")]
        public async Task<IHttpActionResult> GetUserByUserNameAsync(string userName)
        {
            UserDTO user = await _userService.GetByUserNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(int id, UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            UserDTO currentUser = await _userService.GetByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound();
            }
            await _userService.EditAsync(id, user);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            UserDTO currentUser = await _userService.GetByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpPut]
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> AddProjectAsync(int projectId, int userId)
        {
            await _userService.AddProjectAsync(userId, projectId);

            return Ok();
        }

        [HttpDelete]
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> RemoveProjectAsync(int projectId, int userId)

        {
            UserDTO currentUser = await _userService.GetByIdAsync(userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            ProjectDTO currentProject = await _projectService.GetByIdAsync(projectId);
            if (currentProject == null)
            {
                return NotFound();
            }
            await _userService.RemoveProjectAsync(userId, projectId);

            return NotFound();
        }
    }
}
