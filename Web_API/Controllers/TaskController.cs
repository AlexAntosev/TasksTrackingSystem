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
        public void Create(TaskModel task)
        {
            _service.Create(task.Name, task.Description, 0, Convert.ToInt32(task.Priority), task.Deadline);
        }

        //[HttpGet]
        //[Route("api/Task/Get")]
        //public IEnumerable<ProjectDTO> Get()
        //{
        //    return _service.GetAll();
        //}

        // GET: api/Task/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Task
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Task/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Task/5
        public void Delete(int id)
        {
        }
    }
}
