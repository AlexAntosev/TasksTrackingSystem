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
    public class ProjectsControllerTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetAllProjectsAsync_ShouldReturnAllProjects()
        {
            var projects = GetAllProjects();

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
            projectServiceMock.Setup(m => m.GetAllProjectsAsync()).ReturnsAsync(projects);

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            Mock<IUserWithRoleService> userWithRoleServiceMock = new Mock<IUserWithRoleService>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<Project>, List<ProjectDTO>>(It.IsAny<List<Project>>()))
                .Returns(GetAllProjectsDTOs());


            var controller = new ProjectsController(projectServiceMock.Object, userServiceMock.Object, mockMapper.Object, userWithRoleServiceMock.Object);

            var result = await controller.GetProjectsAsync() as OkNegotiatedContentResult<List<ProjectDTO>>;
            Assert.AreEqual(projects.Count, result.Content.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            var projects = GetAllProjects();

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
            projectServiceMock.Setup(m => m.GetProjectByIdAsync(4)).ReturnsAsync(projects[3]);

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            Mock<IUserWithRoleService> userWithRoleServiceMock = new Mock<IUserWithRoleService>();
            
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Project, ProjectDTO>(It.IsAny<Project>()))
                .Returns(GetAllProjectsDTOs()[3]);

            var controller = new ProjectsController(projectServiceMock.Object, userServiceMock.Object, mockMapper.Object, userWithRoleServiceMock.Object);

            var result = await controller.GetProjectByIdAsync(4) as OkNegotiatedContentResult<ProjectDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(projects[3].Name, result.Content.Name);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetNotExistedProduct_ShouldNotFindProduct()
        {
            var projects = GetAllProjects();

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
            projectServiceMock.Setup(m => m.GetProjectByIdAsync(999)).Returns(System.Threading.Tasks.Task.FromResult<Project>(null));

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            Mock<IUserWithRoleService> userWithRoleServiceMock = new Mock<IUserWithRoleService>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new ProjectsController(projectServiceMock.Object, userServiceMock.Object, mockMapper.Object, userWithRoleServiceMock.Object);


            var result = await controller.GetProjectByIdAsync(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        //[TestMethod]
        //public async System.Threading.Tasks.Task CreateProjectAsync_ShouldAddNewProjectToProjectList()
        //{
        //    var authUser = new AuthenticationUser();
        //    var user = new User { Id = 1, AuthenticationUserId = "testId" };
        //    var projects = GetAllProjects();
        //    var projectDTO = new ProjectDTO { Name = "newProject", Tag = "NP", Url = "Url" };
        //    var project = new Project { Name = "newProject", Tag = "NP", Url = "Url" };
        //    Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
        //    projectServiceMock.Setup(m => m.CreateProjectAsync(projectDTO)).ReturnsAsync(project);
        //    Mock<IUserService> userServiceMock = new Mock<IUserService>();
        //    userServiceMock.Setup(m => m.GetUserByAuthenticationIdAsync("testId")).ReturnsAsync(user);
        //    Mock<ApplicationUserManager> applicationUserManagerMock = new Mock<ApplicationUserManager>();
        //    Mock<IUserWithRoleService> userWithRoleServiceMock = new Mock<IUserWithRoleService>();
        //    var expected = new List<ProjectDTO>();
        //    Mock<IMapper> mockMapper = new Mock<IMapper>();
        //    mockMapper.Setup(x => x.Map<Project, ProjectDTO>(It.IsAny<Project>()))
        //        .Returns(projectDTO);

        //    var owinMock = new Mock<IOwinContext>();

        //    var userStoreMock = new Mock<IUserStore<AuthenticationUser>>();
        //    userStoreMock.Setup(s => s.FindByIdAsync("testId")).ReturnsAsync(new AuthenticationUser()
        //    {
        //        Id = "testId",
        //        Email = "test@email.com"
        //    });
        //    var applicationUserManager = new Mock<ApplicationUserManager>(userStoreMock.Object);
        //    applicationUserManager.Setup(s => s.FindByIdAsync()).ReturnsAsync(new AuthenticationUser()
        //    {
        //        Id = "testId",
        //        Email = "test@email.com"
        //    });
        //    owinMock.Setup(o => o.Get<ApplicationUserManager>(It.IsAny<string>())).Returns(applicationUserManager);

        //    var controller = new ProjectsController(projectServiceMock.Object, userServiceMock.Object, mockMapper.Object, userWithRoleServiceMock.Object);
        //    controller.UserManager = applicationUserManager.Object;
        //    var result = await controller.CreateProjectAsync(projectDTO) as OkNegotiatedContentResult<ProjectDTO>;
        //    Assert.AreEqual(projects.Count, 5);
        //    Assert.AreEqual(projects[4].Name, result.Content.Name);
        //    Assert.AreEqual(projects[4].Tag, result.Content.Tag);
        //    Assert.AreEqual(projects[4].Url, result.Content.Url);
        //}

        //[TestMethod]
        //public async System.Threading.Tasks.Task EditProjectAsync_ShouldEditSecondProjectInProjectList()
        //{
        //    var projects = GetAllProjects();
        //    var projectDTO = new ProjectDTO { Id = 1, Name = "editerProject", Tag = "NP", Url = "Url" };
        //    var project = new ProjectDTO { Id = 1, Name = "editerProject", Tag = "NP", Url = "Url" };

        //    Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();
        //    projectServiceMock.Setup(m => m.GetProjectByIdAsync(1)).ReturnsAsync(projects[0]);
        //    Mock<IUserService> userServiceMock = new Mock<IUserService>();
        //    Mock<IUserWithRoleService> userWithRoleServiceMock = new Mock<IUserWithRoleService>();
        //    var expected = new List<ProjectDTO>();
        //    Mock<IMapper> mockMapper = new Mock<IMapper>();
        //    mockMapper.Setup(x => x.Map<List<Project>, List<ProjectDTO>>(It.IsAny<List<Project>>()))
        //        .Returns(GetAllProjectsDTOs());

        //    var controller = new ProjectsController(projectServiceMock.Object, userServiceMock.Object, mockMapper.Object, userWithRoleServiceMock.Object);




        //    var result = await controller.EditProjectAsync(1, project) as OkNegotiatedContentResult<ProjectDTO>;
        //    Assert.AreEqual(projects.Count, 4);
        //    Assert.AreEqual(projects[1].Name, result.Content.Name);
        //    Assert.AreEqual(projects[1].Tag, result.Content.Tag);
        //    Assert.AreEqual(projects[1].Url, result.Content.Url);
        //}

        private List<Project> GetAllProjects()
        {
            var projects = new List<Project>();
            projects.Add(new Project { Id = 1, Name = "Project1", Tag = "P1", Url = "url1"});
            projects.Add(new Project { Id = 2, Name = "Project2", Tag = "P2", Url = "url2" });
            projects.Add(new Project { Id = 3, Name = "Project3", Tag = "P3", Url = "url3" });
            projects.Add(new Project { Id = 4, Name = "Project4", Tag = "P4", Url = "url4" });

            return projects;
        }

        private List<ProjectDTO> GetAllProjectsDTOs()
        {
            var projects = new List<ProjectDTO>();
            projects.Add(new ProjectDTO { Id = 1, Name = "Project1", Tag = "P1", Url = "url1" });
            projects.Add(new ProjectDTO { Id = 2, Name = "Project2", Tag = "P2", Url = "url2" });
            projects.Add(new ProjectDTO { Id = 3, Name = "Project3", Tag = "P3", Url = "url3" });
            projects.Add(new ProjectDTO { Id = 4, Name = "Project4", Tag = "P4", Url = "url4" });

            return projects;
        }
    }
}
