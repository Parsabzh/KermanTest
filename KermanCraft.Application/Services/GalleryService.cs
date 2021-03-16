using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Gallery;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Gallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class GalleryService:IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IMapper _mapper;

        public GalleryService(IGalleryRepository galleryRepository, IMapper mapper)
        {
            _galleryRepository = galleryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<GalleryViewModel>> AddGallery(GalleryViewModel gallery)
        {
            var model = new Gallery()
            {
                Description = gallery.Description,
                EndDate = PersianDate.ToGeorgianDate(gallery.EndDate),
                Name = gallery.Name,
                PeopleId = gallery.PeopleId,
                StartDate = PersianDate.ToGeorgianDate(gallery.StartDate),
                Status = gallery.Status
            };
            var result = await _galleryRepository.AddGallery(model);

            var vm = new GalleryViewModel()
            {
                Description = result.Entity.Description,
                EndDate = PersianDate.ToPersianDateString(result.Entity.EndDate),
                Name = result.Entity.Name,
                PeopleId = gallery.PeopleId,
                StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
                Status = result.Entity.Status
            };
            return new ResponseBase<GalleryViewModel>()
            {
                Entity =vm,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<GalleryViewModel>> UpdateGallery(GalleryViewModel gallery)
        {
            var model = new Gallery()
            {
                Description = gallery.Description,
                EndDate = PersianDate.ToGeorgianDate(gallery.EndDate),
                Name = gallery.Name,
                PeopleId = gallery.PeopleId,
                StartDate = PersianDate.ToGeorgianDate(gallery.StartDate),
                Status = gallery.Status,
                GalleryId = gallery.GalleryId
            };
            var result = await _galleryRepository.UpdateGallery(model);
            var vm = new GalleryViewModel()
            {
                Description = result.Entity.Description,
                EndDate = PersianDate.ToPersianDateString(result.Entity.EndDate),
                Name = result.Entity.Name,
                PeopleId = result.Entity.PeopleId,
                StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
                Status = result.Entity.Status
            };
            return new ResponseBase<GalleryViewModel>()
            {
                Entity = vm,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<GalleryViewModel>> DeleteGallery(int galleryId)
        {
            var result = await _galleryRepository.DeleteGallery(galleryId);
            return new ResponseBase<GalleryViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<GalleryViewModel>> GetGallery(int galleryId)
        {
            var result = await _galleryRepository.GetGallery(galleryId);
            var vm = new GalleryViewModel()
            {
                Description = result.Entity.Description,
                EndDate = PersianDate.ToPersianDateString(result.Entity.EndDate),
                Name = result.Entity.Name,
                PeopleId = result.Entity.PeopleId,
                StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
                Status = result.Entity.Status
            };

            return new ResponseBase<GalleryViewModel>()
            {
                Entity = vm,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<GalleryViewModel>>> GetAllGallery()
        {

            var result = await _galleryRepository.GetAllGallery();
            return new ResponseBase<IEnumerable<GalleryViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<GalleryViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<GalleryViewModel>>> GetArtistGallery(string userId)
        {
            var result = await _galleryRepository.GetArtistGallery(userId);
            return new ResponseBase<IEnumerable<GalleryViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<GalleryViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
