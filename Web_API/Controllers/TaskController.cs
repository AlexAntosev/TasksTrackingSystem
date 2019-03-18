using BLL.DTO;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Web_API.Controllers
{
    public class TaskController : ApiController
    {
        private ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/Task/Create")]
        public IHttpActionResult Create(TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Create(task, projectId);

            return Ok();
        }

        [HttpGet]
        [Route("api/Task/Get")]
        public IHttpActionResult Get()
        {
            IEnumerable<TaskDTO> tasks = _service.GetAll();
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/Project/{id}/Task/Get")]
        public IHttpActionResult GetByProject(int id)
        {
            IEnumerable<TaskDTO> tasks = _service.GetByProject(id);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/Task/Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            TaskDTO task = _service.Get(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut]
        [Route("api/Task/Update/{id}")]
        public IHttpActionResult Update(int id, TaskDTO task, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Update(id, task, projectId);

            return Ok();
        }

        [HttpDelete]
        [Route("api/Task/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.Delete(id);

            return Ok();
        }
    }
}
