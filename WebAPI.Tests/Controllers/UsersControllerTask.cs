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
    public class UsersControllerTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            var users = GetAllUsers();

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetAllUsersAsync()).ReturnsAsync(users);

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<User>, List<UserDTO>>(It.IsAny<List<User>>()))
                .Returns(GetAllUserDTOs);


            var controller = new UsersController(userServiceMock.Object, projectServiceMock.Object, mockMapper.Object);

            var result = await controller.GetAllUsersAsync() as OkNegotiatedContentResult<List<UserDTO>>;
            Assert.AreEqual(users.Count, result.Content.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetUserAsync_ShouldReturnCorrectUser()
        {
            var users = GetAllUsers();

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetUserByIdAsync(4)).ReturnsAsync(users[3]);

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<User, UserDTO>(It.IsAny<User>()))
                .Returns(GetAllUserDTOs()[3]);

            var controller = new UsersController(userServiceMock.Object, projectServiceMock.Object, mockMapper.Object);

            var result = await controller.GetUserByIdAsync(4) as OkNegotiatedContentResult<UserDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(users[3].UserName, result.Content.UserName);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetNotExistedUser_ShouldNotFindUser()
        {
            var users = GetAllUsers();

            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetUserByIdAsync(999))
                .Returns(System.Threading.Tasks.Task.FromResult<User>(null));

            Mock<IProjectService> projectServiceMock = new Mock<IProjectService>();

            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new UsersController(userServiceMock.Object, projectServiceMock.Object, mockMapper.Object);


            var result = await controller.GetUserByIdAsync(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<User> GetAllUsers()
        {
            var users = new List<User>();
            users.Add(new User {Id = 1, UserName = "User1"});
            users.Add(new User {Id = 2, UserName = "User2"});
            users.Add(new User {Id = 3, UserName = "User3"});
            users.Add(new User {Id = 4, UserName = "User4"});

            return users;
        }

        private List<UserDTO> GetAllUserDTOs()
        {
            var users = new List<UserDTO>();
            users.Add(new UserDTO {Id = 1, UserName = "User1"});
            users.Add(new UserDTO {Id = 2, UserName = "User2"});
            users.Add(new UserDTO {Id = 3, UserName = "User3"});
            users.Add(new UserDTO {Id = 4, UserName = "User4"});

            return users;
        }
    }
}
