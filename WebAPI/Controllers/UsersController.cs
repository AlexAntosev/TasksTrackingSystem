//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using BLL.DTO;
//using BLL.Interfaces;
//using DAL.Entities;
//using Microsoft.AspNet.Identity;

//namespace WebAPI.Controllers
//{
//    public class UsersController : ApiController
//    {
//        private IUserService _service;

//        public UsersController(IUserService service)
//        {
//            _service = service;
//        }

//        [HttpPost]
//        public IHttpActionResult AddToProject(int userid, int projectId)
//        {
//            //ApplicationUser user = UserManager.FindByIdAsync(User.Identity.GetUserId());

//            return Ok(); //Created();
//        }

//        [HttpGet]
//        public IHttpActionResult GetTasks()
//        {
//            IEnumerable<TaskDTO> tasks = _service.GetAll();
//            if (tasks == null)
//            {
//                return NotFound();
//            }

//            return Ok(tasks);
//        }

//        [HttpGet]
//        [Route("api/Projects/{projectId}/Tasks")]
//        public IHttpActionResult GetTasksByProjectId(int projectId)
//        {
//            IEnumerable<TaskDTO> tasks = _service.GetByProject(projectId);
//            if (tasks == null)
//            {
//                return NotFound();
//            }

//            return Ok(tasks);
//        }

//        [HttpGet]
//        public IHttpActionResult GetTaskById(int id)
//        {
//            TaskDTO task = _service.Get(id);
//            if (task == null)
//            {
//                return NotFound();
//            }

//            return Ok(task);
//        }

//        [HttpPut]
//        public IHttpActionResult Update(int id, TaskDTO task, int projectId)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("Model is not valid.");
//            }

//            TaskDTO currentTask = _service.Get(id);
//            if (currentTask == null)
//            {
//                return NotFound();
//            }
//            _service.Update(id, task, projectId);

//            return Ok();
//        }

//        [HttpDelete]
//        public IHttpActionResult Delete(int id)
//        {
//            TaskDTO currentTask = _service.Get(id);
//            if (currentTask == null)
//            {
//                return NotFound();
//            }
//            _service.Delete(id);

//            return StatusCode(HttpStatusCode.NoContent);
//        }
//    }
//}
