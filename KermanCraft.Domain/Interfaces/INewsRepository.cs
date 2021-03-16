using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.News;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<ResponseBase<News>> AddNews(News news);
        Task<ResponseBase<News>> UpdateNews(News news);
        Task<ResponseBase<News>> DeleteNews(int newsId);
        Task<ResponseBase<News>> GetNews(int newsId);
        Task<ResponseBase<IEnumerable<News>>> GetAllNews();
    }
}
