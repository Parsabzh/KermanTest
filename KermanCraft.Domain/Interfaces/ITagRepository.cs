using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Tag;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<ResponseBase<Tag>> AddTag(Tag tag);
        Task<ResponseBase<Tag>> UpdateTag(Tag tag);
        Task<ResponseBase<Tag>> DeleteTag(int tagId);
        Task<ResponseBase<Tag>> GetTag(int tagId);
        Task<ResponseBase<IEnumerable<Tag>>> GetProductTag(int productId);
        Task<ResponseBase<IEnumerable<Tag>>> GetNewsTag(int newsId);
        Task<ResponseBase<IEnumerable<Tag>>> GetArticleTag(int articleId);

        Task<ResponseBase<IEnumerable<Tag>>> GetAllTag();
    }
}
