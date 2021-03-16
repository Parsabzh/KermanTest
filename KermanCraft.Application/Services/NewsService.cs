using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.News;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.News;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class NewsService:INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        private IImageService _imageService;

        public NewsService(IMapper mapper, INewsRepository newsRepository, IImageService imageService)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
            _imageService = imageService;
        }

        public async Task<ResponseBase<NewsViewModel>> AddNews(NewsViewModel news)
        {
          
            var result = await _newsRepository.AddNews(_mapper.Map<News>(news));
            return new ResponseBase<NewsViewModel>()
            {
                Entity = _mapper.Map<NewsViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<UpdateNewsViewModel>> UpdateNews(UpdateNewsViewModel news)
        {
          
            var result = await _newsRepository.UpdateNews(_mapper.Map<News>(news));
            return new ResponseBase<UpdateNewsViewModel>()
            {
                Entity = _mapper.Map<UpdateNewsViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<NewsViewModel>> DeleteNews(int newsId)
        {
            var result = await _newsRepository.DeleteNews(newsId);
            return new ResponseBase<NewsViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<NewsViewModel>> GetNews(int newsId)
        {
            var result = await _newsRepository.GetNews(newsId);
            return new ResponseBase<NewsViewModel>()
            {
                Entity = _mapper.Map<NewsViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<NewsViewModel>>> GetAllNews()
        {

            var result = await _newsRepository.GetAllNews();
            var entity = _mapper.Map<IEnumerable<NewsViewModel>>(result.Entity);
            var newsViewModels = entity.ToList();
            foreach (var item in newsViewModels)
            {
                var image = await _imageService.GetNewsImage(item.NewsId);
                item.ImageViewModels = image.Entity.ToList();
            }
            return new ResponseBase<IEnumerable<NewsViewModel>>()
            {
                Entity =newsViewModels ,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
