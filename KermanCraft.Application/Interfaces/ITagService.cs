using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Application.ViewModels.Tag;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ITagService
    {
        Task<ResponseBase<TagViewModel>> AddTag(TagViewModel tag);
        Task<ResponseBase<TagViewModel>> UpdateTag(TagViewModel tag);
        Task<ResponseBase<TagViewModel>> DeleteTag(int tagId);
        Task<ResponseBase<TagViewModel>> GetTag(int tagId);
        Task<ResponseBase<IEnumerable<TagViewModel>>> GetProductTag(int productId);
        Task<ResponseBase<IEnumerable<TagViewModel>>> GetNewsTag(int newsId);
        Task<ResponseBase<IEnumerable<TagViewModel>>> GetArticleTag(int articleId);
        Task<ResponseBase<IEnumerable<TagViewModel>>> GetAllTag();
    }
}
