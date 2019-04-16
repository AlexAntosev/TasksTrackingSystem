using AutoMapper;
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
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCommentsAsync()
        {
            List<Comment> comments = await _commentService.GetAllCommentsAsync();
            if (comments == null)
            {
                return NotFound();
            }

            var commentsDTO = _mapper.Map<List<Comment>, List<CommentDTO>>(comments);
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

            var commentDTO = _mapper.Map<Comment, CommentDTO>(comment);
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

            var commentsDTO = _mapper.Map<List<Comment>, List<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCommentAsync(CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The comment is not created. The comment creating model is incorrectly filled.");
            }

            Comment createdComment = await _commentService.CreateCommentAsync(commentDTO);
            CommentDTO createdCommentDTO = _mapper.Map<Comment, CommentDTO>(createdComment);

            return Created(Url.Request.RequestUri, createdCommentDTO);
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
                return BadRequest("The comment is not edited. The comment editing model is incorrectly filled.");
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
