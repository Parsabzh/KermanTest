using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.News;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface INewsService
    {
        Task<ResponseBase<NewsViewModel>> AddNews(NewsViewModel news);
        Task<ResponseBase<UpdateNewsViewModel>> UpdateNews(UpdateNewsViewModel news);
        Task<ResponseBase<NewsViewModel>> DeleteNews(int newsId);
        Task<ResponseBase<NewsViewModel>> GetNews(int newsId);
        Task<ResponseBase<IEnumerable<NewsViewModel>>> GetAllNews();
    }
}
