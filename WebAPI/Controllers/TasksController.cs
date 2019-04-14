using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;

namespace WebAPI.Controllers
{
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ITaskService _service;
        private readonly IMapper _mapper;

        public TasksController(ITaskService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateTaskAsync(TaskDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            await _service.CreateTaskAsync(task);
            
            return Created(Url.Request.RequestUri, task);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllTasksAsync()
        {
            List<Task> tasks = await _service.GetAllTasksAsync();
            if (tasks == null)
            {
                return NotFound();
            }

            List<TaskDTO> tasksDTO = _mapper.Map<List<Task>, List<TaskDTO>>(tasks);
            return Ok(tasksDTO);
        }

        [HttpGet]
        [Route("api/Projects/{projectId}/Tasks")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllTasksByProjectIdAsync(int projectId)
        {
            List<Task> tasks = await _service.GetAllTasksByProjectIdAsync(projectId);
            if (tasks == null)
            {
                return NotFound();
            }

            var tasksDTO = _mapper.Map<List<Task>, List<TaskDTO>>(tasks);
            return Ok(tasksDTO);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetTaskByIdAsync(int id)
        {
            Task task = await _service.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            TaskDTO taskDTO = _mapper.Map<Task, TaskDTO>(task);
            return Ok(taskDTO);
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> UpdateTaskAsync(int id, TaskDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            Task currentTask = await _service.GetTaskByIdAsync(id);
            if (currentTask == null)
            {
                return NotFound();
            }
            await _service.UpdateTaskAsync(id, task);

            return Ok(task);
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task<IHttpActionResult> DeleteTaskAsync(int id)
        {
            Task currentTask = await _service.GetTaskByIdAsync(id);
            if (currentTask == null)
            {
                return NotFound();
            }

            await _service.DeleteTaskAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
