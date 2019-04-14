using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;

namespace WebAPI.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IProjectService projectService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> GetAllUsersByProjectIdAsync(int projectId)
        {
            var users = await _userService.GetAllUsersByProjectIdAsync(projectId);
            if (users == null)
            {
                return NotFound();
            }

            List<UserDTO> usersDTO = _mapper.Map<List<User>, List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsersAsync()
        {
            List<User> users = await _userService.GetAllUsersAsync();
            if (users == null)
            {
                return NotFound();
            }

            List<UserDTO> usersDTO = _mapper.Map<List<User>, List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        [HttpGet]
        [Route("api/Users/UserName/{userName}")]
        public async Task<IHttpActionResult> GetUserByUserNameAsync(string userName)
        {
            User user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserByIdAsync(int id)
        {
            User user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }


        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserAsync(int id, UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            User currentUser = await _userService.GetUserByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound();
            }

            await _userService.EditUserAsync(id, user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUserAsync(int id)
        {
            User currentUser = await _userService.GetUserByIdAsync(id);
            if (currentUser == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpPut]
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> AddProjectToUserAsync(int projectId, int userId)
        {
            await _userService.AddProjectToUserAsync(userId, projectId);

            return Ok();
        }

        [HttpDelete]
        [Route("api/Projects/{projectId}/Users")]
        public async Task<IHttpActionResult> RemoveProjectFromUserAsync(int projectId, int userId)

        {
            User currentUser = await _userService.GetUserByIdAsync(userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            Project currentProject = await _projectService.GetProjectByIdAsync(projectId);
            if (currentProject == null)
            {
                return NotFound();
            }
            await _userService.RemoveProjectFromUserAsync(userId, projectId);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
