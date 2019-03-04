using BLL.Services;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using DAL.EF;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class TaskController : ApiController
    {
        [HttpPost]
        [Route("api/Task/Create")]
        public void Create()
        {
            ProjectService service = new ProjectService(new UnitOfWork(new CompanyContext()));
            service.Create(new ProjectDTO()
            {
                Name = "project1",
                Tag = "PR-1"
            });
        }

        // GET: api/Task
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
