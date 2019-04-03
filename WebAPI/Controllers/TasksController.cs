using BLL.DTO;
using BLL.Interfaces;
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
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateAsync(TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            await _service.CreateAsync(task, projectId);

            return Ok(task); //Created();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllTasksAsync()
        {
            IEnumerable<TaskDTO> tasks = await _service.GetAllAsync();
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/Projects/{projectId}/Tasks")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetTasksByProjectIdAsync(int projectId)
        {
            IEnumerable<TaskDTO> tasks = await _service.GetAllByProjectIdAsync(projectId);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetTaskByIdAsync(int id)
        {
            TaskDTO task = await _service.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> UpdateAsync(int id, TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            TaskDTO currentTask = await _service.GetByIdAsync(id);
            if (currentTask == null)
            {
                return NotFound();
            }
            await _service.UpdateAsync(id, task, projectId);

            return Ok(task);
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task<IHttpActionResult> DeleteAsync(int id)
        {
            TaskDTO currentTask = await _service.GetByIdAsync(id);
            if (currentTask == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
