using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCommentsAsync()
        {
            List<Comment> comments = await _commentService.GetAllCommentsAsync();
            if (comments == null)
            {
                return NotFound();
            }

            var commentsDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Comment>, List<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCommentByIdAsync(int id)
        {
            Comment comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<Comment, CommentDTO>(comment);
            return Ok(commentDTO);
        }

        [HttpGet]
        [Route("api/Tasks/{taskId}/Comments")]
        public async Task<IHttpActionResult> GetAllCommentsByTaskIdAsync(int taskId)
        {
            List<Comment> comments = await _commentService.GetAllCommentsByTaskIdAsync(taskId);
            if (comments == null)
            {
                return NotFound();
            }

            var commentsDTO = BLL.Mapper.AutoMapperConfig.Mapper.Map<List<Comment>, List<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCommentAsync(CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Comment is not valid");
            }

            await _commentService.CreateCommentAsync(commentDTO);
            return Ok(commentDTO);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCommentAsync(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentService.DeleteCommentAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditCommentAsync(int id, CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Comment is not valid");
            }

            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.EditCommentASync(id, commentDTO);
            return Ok(commentDTO);
        }
    }
}
