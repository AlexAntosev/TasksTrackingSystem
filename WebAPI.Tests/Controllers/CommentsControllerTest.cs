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
    public class CommentsControllerTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task GetAllCommentsAsync_ShouldReturnAllComments()
        {
            var comments = GetAllComments();

            Mock<ICommentService> commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(m => m.GetAllCommentsAsync()).ReturnsAsync(comments);
            
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<Comment>, List<CommentDTO>>(It.IsAny<List<Comment>>()))
                .Returns(GetAllCommentsDTOs());


            var controller = new CommentsController(commentServiceMock.Object, mockMapper.Object);

            var result = await controller.GetAllCommentsAsync() as OkNegotiatedContentResult<List<CommentDTO>>;
            Assert.AreEqual(comments.Count, result.Content.Count);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetCommentAsync_ShouldReturnCorrectComment()
        {
            var comments = GetAllComments();

            Mock<ICommentService> commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(m => m.GetCommentByIdAsync(4)).ReturnsAsync(comments[3]);

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Comment, CommentDTO>(It.IsAny<Comment>()))
                .Returns(GetAllCommentsDTOs()[3]);

            var controller = new CommentsController(commentServiceMock.Object, mockMapper.Object);

            var result = await controller.GetCommentByIdAsync(4) as OkNegotiatedContentResult<CommentDTO>;
            Assert.IsNotNull(result);
            Assert.AreEqual(comments[3].Description, result.Content.Description);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetNotExistedComment_ShouldNotFindComment()
        {
            var comments = GetAllComments();

            Mock<ICommentService> commentServiceMock = new Mock<ICommentService>();
            commentServiceMock.Setup(m => m.GetCommentByIdAsync(999)).Returns(System.Threading.Tasks.Task.FromResult<Comment>(null));
            
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var controller = new CommentsController(commentServiceMock.Object, mockMapper.Object);


            var result = await controller.GetCommentByIdAsync(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Comment> GetAllComments()
        {
            var comments = new List<Comment>();
            comments.Add(new Comment { Id = 1, Description = "Comment1"});
            comments.Add(new Comment { Id = 2, Description = "Comment2"});
            comments.Add(new Comment { Id = 3, Description = "Comment3"});
            comments.Add(new Comment { Id = 4, Description = "Comment4"});

            return comments;
        }

        private List<CommentDTO> GetAllCommentsDTOs()
        {
            var comments = new List<CommentDTO>();
            comments.Add(new CommentDTO { Id = 1, Description = "Comment1"});
            comments.Add(new CommentDTO { Id = 2, Description = "Comment2"});
            comments.Add(new CommentDTO { Id = 3, Description = "Comment3"});
            comments.Add(new CommentDTO { Id = 4, Description = "Comment4"});

            return comments;
        }
    }
}
