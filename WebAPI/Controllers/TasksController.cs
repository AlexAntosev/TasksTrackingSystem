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
        public IHttpActionResult Create(TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }
            _service.Create(task, projectId);

            return Ok(task); //Created();
        }

        [HttpGet]
        public IHttpActionResult GetTasks()
        {
            IEnumerable<TaskDTO> tasks = _service.GetAll();
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/Projects/{projectId}/Tasks")]
        public IHttpActionResult GetTasksByProjectId(int projectId)
        {
            IEnumerable<TaskDTO> tasks = _service.GetByProject(projectId);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        public IHttpActionResult GetTaskById(int id)
        {
            TaskDTO task = _service.Get(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid.");
            }

            TaskDTO currentTask = _service.Get(id);
            if (currentTask == null)
            {
                return NotFound();
            }
            _service.Update(id, task, projectId);

            return Ok(task);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            TaskDTO currentTask = _service.Get(id);
            if (currentTask == null)
            {
                return NotFound();
            }
            _service.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
