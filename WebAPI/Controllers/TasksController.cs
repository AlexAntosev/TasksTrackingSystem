using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TasksController : ApiController
    {
        private ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateTaskAsync(TaskDTO task, int projectId, int creatorUserId, int executorUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            await _service.CreateTaskAsync(task, projectId, creatorUserId, executorUserId);

            return Ok(task); //Created();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllTasksAsync()
        {
            List<Task> tasks = await _service.GetAllTasksAsync();
            if (tasks == null)
            {
                return NotFound();
            }

            List<TaskDTO> tasksDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Task>, List<TaskDTO>>(tasks);
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

            List<TaskDTO> tasksDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Task>, List<TaskDTO>>(tasks);
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

            TaskDTO taskDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<Task, TaskDTO>(task);
            return Ok(taskDTO);
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> UpdateTaskAsync(int id, TaskDTO task, int projectId, int creatorUserId, int executorUserId)
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
            await _service.UpdateTaskAsync(id, task, projectId, creatorUserId, executorUserId);

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
