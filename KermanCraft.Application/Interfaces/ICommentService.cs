using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ICommentService
    {
        Task<ResponseBase<CommentViewModel>> AddComment(CommentViewModel comment);
        Task<ResponseBase<CommentViewModel>> UpdateComment(CommentViewModel comment);
        Task<ResponseBase<CommentViewModel>> DeleteComment(int commentId);
        Task<ResponseBase<CommentViewModel>> GetComment(int commentId);
        Task<ResponseBase<IEnumerable<CommentViewModel>>> GetAllCommentByType(string type);
        Task<ResponseBase<IEnumerable<CommentViewModel>>> GetAllComment();
        Task<ResponseBase<IEnumerable<CommentViewModel>>> GetCustomerComment(string userId);
        Task<ResponseBase<IEnumerable<CommentViewModel>>> GetTypeComment(string type, int typeId);

    }
}
