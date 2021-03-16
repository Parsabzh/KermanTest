using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<ResponseBase<CommentViewModel>> AddComment(CommentViewModel comment)
        {
            var result = await _commentRepository.AddComment(_mapper.Map<Comment>(comment));
            return new ResponseBase<CommentViewModel>()
            {
                Entity = _mapper.Map<CommentViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<CommentViewModel>> UpdateComment(CommentViewModel comment)
        {
            var result = await _commentRepository.UpdateComment(_mapper.Map<Comment>(comment));
            return new ResponseBase<CommentViewModel>()
            {
                Entity = _mapper.Map<CommentViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CommentViewModel>> DeleteComment(int commentId)
        {
            var result = await _commentRepository.DeleteComment(commentId);
            return new ResponseBase<CommentViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CommentViewModel>> GetComment(int commentId)
        {
            var result = await _commentRepository.GetComment(commentId);
            return new ResponseBase<CommentViewModel>()
            {
                Entity = _mapper.Map<CommentViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CommentViewModel>>> GetAllCommentByType(string type)
        {

            var result = await _commentRepository.GetAllComment();
            var model = result.Entity.Where(i => i.Type == type);
            return new ResponseBase<IEnumerable<CommentViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CommentViewModel>>(model),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CommentViewModel>>> GetAllComment()
        {
            var result = await _commentRepository.GetAllComment();
            return new ResponseBase<IEnumerable<CommentViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CommentViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CommentViewModel>>> GetCustomerComment(string userId)
        {
            var result = await _commentRepository.GetCustomerComment(userId);
            return new ResponseBase<IEnumerable<CommentViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CommentViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CommentViewModel>>> GetTypeComment(string type, int typeId)
        {
            var result = await _commentRepository.GetTypeComment(type,typeId);
            return new ResponseBase<IEnumerable<CommentViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CommentViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
