using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Image;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
   public class ImageService:IImageService
   {
       private readonly IImageRepository _imageRepository;
       private readonly IMapper _mapper;

       public ImageService(IImageRepository imageRepository, IMapper mapper)
       {
           _imageRepository = imageRepository;
           _mapper = mapper;
       }

       public async Task<ResponseBase<ImageViewModel>> AddImage(ImageViewModel img)
       {
           var image=_mapper.Map<Image>(img);
           var result = await _imageRepository.AddImage(image);
           return new ResponseBase<ImageViewModel>()
           {
               Entity = _mapper.Map<ImageViewModel>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
       }

       public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetWindowsImage(int winId)
       {
           var result = await _imageRepository.GetWindowsImage(winId);
           return new ResponseBase<IEnumerable<ImageViewModel>>()
           {
               Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
       }

       public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetNewsImage(int newsId)
       {
           var result = await _imageRepository.GetNewsImage(newsId);
           return new ResponseBase<IEnumerable<ImageViewModel>>()
           {
               Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
        }

       public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetGalleryImage(int galleryId)
       {
           var result = await _imageRepository.GetGalleryImage(galleryId);
           return new ResponseBase<IEnumerable<ImageViewModel>>()
           {
               Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
        }

       public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetArtGalleryImage(int artGallery)
       {
           var result = await _imageRepository.GetArtGalleryImage(artGallery);
           return new ResponseBase<IEnumerable<ImageViewModel>>()
           {
               Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
        }

       public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetProductImage(int productId)
       {
           var result = await _imageRepository.GetProductImage(productId);
           return new ResponseBase<IEnumerable<ImageViewModel>>()
           {
               Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
               IsSuccessful = result.IsSuccessful,
               Message = result.Message

           };
        }

       public async Task<ResponseBase<ImageViewModel>> DeleteImage(int imgId)
        {
            var result = await _imageRepository.DeleteImage(imgId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteProductImage(int productId)
        {
            var result = await _imageRepository.DeleteProductImage(productId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteWindowsImage(int windowId)
        {
            var result = await _imageRepository.DeleteWindowsImage(windowId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteNewsImage(int newsId)
        {
            var result = await _imageRepository.DeleteNewsImage( newsId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteGalleryImage(int galleryId)
        {
            var result = await _imageRepository.DeleteGalleryImage(galleryId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteArtGalleryImage(int artGalleryId)
        {
            var result = await _imageRepository.DeleteArtGalleryImage(artGalleryId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<IEnumerable<ImageViewModel>>> GetArticleImage(int articleId)
        {
            var result = await _imageRepository.GetArticleImage(articleId);
            return new ResponseBase<IEnumerable<ImageViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<ImageViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }

        public async Task<ResponseBase<ImageViewModel>> DeleteArticleImage(int articleId)
        {
            var result = await _imageRepository.DeleteArticleImage(articleId);
            return new ResponseBase<ImageViewModel>()
            {
                Entity = _mapper.Map<ImageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message

            };
        }
   }
}
