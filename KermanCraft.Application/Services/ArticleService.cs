using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Article;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Article;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class ArticleService:IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ArticleService(IMapper mapper, IArticleRepository articleRepository, IImageService imageService)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _imageService = imageService;
        }

        public async Task<ResponseBase<ArticleViewModel>> AddArticle(ArticleViewModel article)
        {
            //article.Date = PersianDate.ToGeorgianDate(article.Date).Date.ToString();
            var result = await _articleRepository.AddArticle(_mapper.Map<Article>(article));
            return new ResponseBase<ArticleViewModel>()
            {
                Entity = _mapper.Map<ArticleViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<ArticleViewModel>> UpdateArticle(ArticleViewModel article)
        {
            //article.Date = PersianDate.ToGeorgianDate(article.Date).Date.ToString();
            var result = await _articleRepository.UpdateArticle(_mapper.Map<Article>(article));
            return new ResponseBase<ArticleViewModel>()
            {
                Entity = _mapper.Map<ArticleViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ArticleViewModel>> DeleteArticle(int articleId)
        {
            var result = await _articleRepository.DeleteArticle(articleId);
            return new ResponseBase<ArticleViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ArticleViewModel>> GetArticle(int articleId)
        {
            var result = await _articleRepository.GetArticle(articleId);
            return new ResponseBase<ArticleViewModel>()
            {
                Entity = _mapper.Map<ArticleViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ArticleViewModel>>> GetAllArticle()
        {

            var result = await _articleRepository.GetAllArticle();
            var entity = _mapper.Map<IEnumerable<ArticleViewModel>>(result.Entity);
            var articleViewModels = entity.ToList();
            var newsViewModels = articleViewModels.ToList();
            foreach (var item in newsViewModels)
            {
                var image = await _imageService.GetArticleImage(item.ArticleId);
                item.ImageViewModels = image.Entity.ToList();
            }
            return new ResponseBase<IEnumerable<ArticleViewModel>>()
            {
                Entity = articleViewModels,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ArticleViewModel>>> GetArtistArticle(string userId)
        {
            var result = await _articleRepository.GetArtistArticle(userId);
            return new ResponseBase<IEnumerable<ArticleViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<ArticleViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
