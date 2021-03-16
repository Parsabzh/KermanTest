using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Comment;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<ResponseBase<Comment>> AddComment(Comment comment);
        Task<ResponseBase<Comment>> UpdateComment(Comment comment);
        Task<ResponseBase<Comment>> DeleteComment(int commentId);
        Task<ResponseBase<Comment>> GetComment(int commentId);
        Task<ResponseBase<IEnumerable<Comment>>> GetAllComment();
 
        Task<ResponseBase<IEnumerable<Comment>>> GetCustomerComment(string userId);
        Task<ResponseBase<IEnumerable<Comment>>> GetTypeComment(string type,int typeId);




    }
}
