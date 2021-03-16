using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Article;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IArticleRepository
    {
        Task<ResponseBase<Article>> AddArticle(Article article);
        Task<ResponseBase<Article>> UpdateArticle(Article article);
        Task<ResponseBase<Article>> DeleteArticle(int articleId);
        Task<ResponseBase<Article>> GetArticle(int articleId);
        Task<ResponseBase<IEnumerable<Article>>> GetArtistArticle(string userId);

        Task<ResponseBase<IEnumerable<Article>>> GetAllArticle();

    }
}
