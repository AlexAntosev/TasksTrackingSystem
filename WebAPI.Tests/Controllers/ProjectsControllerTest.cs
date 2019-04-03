using BLL.DTO;
using BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    [TestClass]
    public class ProjectsControllerTest
    {
        [TestMethod]
        public void GetProjects_ReturnsAllProjects()
        {
            //// Arrange
            //Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
            //projectServiceMock.Setup(m => m.GetAll()).Returns(new List<ProjectDTO>()
            //{
            //    new ProjectDTO() {Id = 1, Name = "Project1", Tag = "PR1", Tasks = new TaskDTO[0], Team = new UserDTO[0]},
            //    new ProjectDTO() {Id = 2, Name = "Project2", Tag = "PR2", Tasks = new TaskDTO[0], Team = new UserDTO[0]},
            //    new ProjectDTO() {Id = 3, Name = "Project3", Tag = "PR3", Tasks = new TaskDTO[0], Team = new UserDTO[0]},
            //});
            //ProjectsController controller = new ProjectsController(projectServiceMock.Object);

            //// Act
            //var result = controller.GetProjects() as OkNegotiatedContentResult<IEnumerable<ProjectDTO>>;

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Project1", result.Content.ElementAt(0).Name);
            //Assert.AreEqual("PR2", result.Content.ElementAt(1).Tag);
            //Assert.AreEqual(3, result.Content.Count());
        }
    }
}
