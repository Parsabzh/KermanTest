using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Article;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IArticleService
    {
        Task<ResponseBase<ArticleViewModel>> AddArticle(ArticleViewModel article);
        Task<ResponseBase<ArticleViewModel>> UpdateArticle(ArticleViewModel article);
        Task<ResponseBase<ArticleViewModel>> DeleteArticle(int articleId);
        Task<ResponseBase<ArticleViewModel>> GetArticle(int articleId);
        Task<ResponseBase<IEnumerable<ArticleViewModel>>> GetAllArticle();
        Task<ResponseBase<IEnumerable<ArticleViewModel>>> GetArtistArticle(string userId);
    }
}
