using BLL.Services;
using DAL.EF;
using DAL.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using BLL.Interfaces;
using Web_API.Models;
using BLL.DTO;
using System;

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
        public void Create(TaskDTO task, int projectId)
        {
            _service.Create(task, projectId);
        }

        [HttpGet]
        [Route("api/Task/Get")]
        public IEnumerable<TaskDTO> Get()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("api/Project/{id}/Task/Get")]
        public IEnumerable<TaskDTO> GetByProject(int id)
        {
            return _service.GetByProject(id);
        }

        [HttpGet]
        [Route("api/Task/Get/{id}")]
        public TaskDTO Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPut]
        [Route("api/Task/Update/{id}")]
        public void Update(int id, TaskDTO task, int projectId)
        {
            _service.Update(id, task, projectId);
        }

        [HttpDelete]
        [Route("api/Task/Delete/{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
