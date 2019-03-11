using BLL.DTO;
using BLL.Services;
using DAL.EF;
using DAL.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using BLL.Interfaces;

namespace Web_API.Controllers
{
    public class TaskController : ApiController
    {
        private IProjectService _service;

        public TaskController(IProjectService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/Task/Create")]
        public void Create()
        {
            _service.Create("project2", "DEV-2");
        }

        [HttpGet]
        [Route("api/Task/Get")]
        public IEnumerable<ProjectDTO> Get()
        {
            return _service.GetAll();
        }

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
