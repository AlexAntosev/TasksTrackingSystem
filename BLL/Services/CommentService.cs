using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BLL.Services
{
    public sealed class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                throw new ArgumentNullException(nameof(commentDTO));
            }

            if (commentDTO.Description == null || commentDTO.Time == null)
            {
                throw new ArgumentNullException("Some properties of comment are empty");
            }

            Comment comment = _mapper.Map<CommentDTO, Comment>(commentDTO);

            _unitOfWork.Comments.Create(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            _unitOfWork.Comments.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditCommentASync(int id, CommentDTO commentDTO)
        {
            if (commentDTO == null)
            {
                throw new ArgumentNullException(nameof(commentDTO));
            }

            if (commentDTO.Description == null || commentDTO.Time == null)
            {
                throw new ArgumentNullException("Some properties of comment are empty");
            }

            var comment = _unitOfWork.Comments.GetById(id);
            if (comment == null)
            {
                throw new ArgumentNullException("Comment is not exist.");
            }

            if (comment.Description != comment.Description)
                comment.Description = comment.Description;
            
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _unitOfWork.Comments.GetAllAsync();
        }

        public async Task<List<Comment>> GetAllCommentsByTaskIdAsync(int taskId)
        {
            return await _unitOfWork.Comments.GetAllCommentsByTaskIdAsync(taskId);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _unitOfWork.Comments.GetByIdAsync(id);
        }
    }
}
