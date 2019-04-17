using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    [TestClass]
    public class TasksControllerTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            var tasks = GetAllTasks();

            Mock<ITaskService> taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(m => m.GetAllTasksAsync()).ReturnsAsync(tasks);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<Task>, List<TaskDTO>>(It.IsAny<List<Task>>()))
                .Returns(GetAllTasksDTOs());


            var controller = new TasksController(taskServiceMock.Object, mockMapper.Object);

            var result = await controller.GetAllTasksAsync() as OkNegotiatedContentResult<List<TaskDTO>>;
            Assert.AreEqual(tasks.Count, result.Content.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTaskAsync_ShouldReturnCorrectTask()
        {
            var tasks = GetAllTasks();

            Mock<ITaskService> taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(m => m.GetTaskByIdAsync(4)).ReturnsAsync(tasks[3]);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Task, TaskDTO>(It.IsAny<Task>()))
                .Returns(GetAllTasksDTOs()[3]);

            var controller = new TasksController(taskServiceMock.Object, mockMapper.Object);

            var result = await controller.GetTaskByIdAsync(4) as OkNegotiatedContentResult<TaskDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(tasks[3].Description, result.Content.Description);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetNotExistedTask_ShouldNotFindTask()
        {
            var tasks = GetAllTasks();

            Mock<ITaskService> taskServiceMock = new Mock<ITaskService>();
            taskServiceMock.Setup(m => m.GetTaskByIdAsync(999)).Returns(System.Threading.Tasks.Task.FromResult<Task>(null));

            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new TasksController(taskServiceMock.Object, mockMapper.Object);


            var result = await controller.GetTaskByIdAsync(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        
        private List<Task> GetAllTasks()
        {
            var tasks = new List<Task>();
            tasks.Add(new Task { Id = 1, Name = "task1", Description = "Comment1" });
            tasks.Add(new Task { Id = 2, Name = "task2", Description = "Comment2" });
            tasks.Add(new Task { Id = 3, Name = "task3", Description = "Comment3" });
            tasks.Add(new Task { Id = 4, Name = "task4", Description = "Comment4" });

            return tasks;
        }

        private List<TaskDTO> GetAllTasksDTOs()
        {
            var tasks = new List<TaskDTO>();
            tasks.Add(new TaskDTO { Id = 1, Name = "task1", Description = "Comment1" });
            tasks.Add(new TaskDTO { Id = 2, Name = "task2", Description = "Comment2" });
            tasks.Add(new TaskDTO { Id = 3, Name = "task3", Description = "Comment3" });
            tasks.Add(new TaskDTO { Id = 4, Name = "task4", Description = "Comment4" });

            return tasks;
        }
    }
}
