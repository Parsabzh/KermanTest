using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.ArtGallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
   public class ArtGalleryService:IArtGalleryService
   {
       private readonly IArtGalleryRepository _artGalleryRepository;
       private readonly IMapper _mapper;
       private IImageService _imageService;
       public ArtGalleryService(IArtGalleryRepository artGalleryRepository, IMapper mapper, IImageService imageService)
       {
           _artGalleryRepository = artGalleryRepository;
           _mapper = mapper;
           _imageService = imageService;
       }

        public async Task<ResponseBase<ArtGalleryViewModel>> AddArtGallery(ArtGalleryViewModel artGallery)
        {
            var result = await _artGalleryRepository.AddArtGallery(_mapper.Map<ArtGallery>(artGallery));
            return new ResponseBase<ArtGalleryViewModel>()
            {
                Entity = _mapper.Map<ArtGalleryViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<ArtGalleryViewModel>> UpdateArtGallery(ArtGalleryViewModel artGallery)
        {
            var result = await _artGalleryRepository.UpdateArtGallery(_mapper.Map<ArtGallery>(artGallery));
            return new ResponseBase<ArtGalleryViewModel>()
            {
                Entity = _mapper.Map<ArtGalleryViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ArtGalleryViewModel>> DeleteArtGallery(int artGalleryId)
        {
            var result = await _artGalleryRepository.DeleteArtGallery(artGalleryId);
            return new ResponseBase<ArtGalleryViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ArtGalleryViewModel>> GetArtGallery(int artGalleryId)
        {
            var result = await _artGalleryRepository.GetArtGallery(artGalleryId);
            return new ResponseBase<ArtGalleryViewModel>()
            {
                Entity = _mapper.Map<ArtGalleryViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ArtGalleryViewModel>>> GetAllArtGallery()
        {

            var result = await _artGalleryRepository.GetAllArtGallery();
            var entity = _mapper.Map<IEnumerable<ArtGalleryViewModel>>(result.Entity);
            var artGalleryViewModels = entity.ToList();
            foreach (var item in artGalleryViewModels)
            {
                var image = await _imageService.GetArtGalleryImage(item.ArtGalleryId);
                item.ImageViewModels = image.Entity.ToList();
            }
            return new ResponseBase<IEnumerable<ArtGalleryViewModel>>()
            {
                Entity = artGalleryViewModels,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
