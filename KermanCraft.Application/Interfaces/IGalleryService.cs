using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Gallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
   public interface IGalleryService
    {
        Task<ResponseBase<GalleryViewModel>> AddGallery(GalleryViewModel gallery);
        Task<ResponseBase<GalleryViewModel>> UpdateGallery(GalleryViewModel gallery);
        Task<ResponseBase<GalleryViewModel>> DeleteGallery(int galleryId);
        Task<ResponseBase<GalleryViewModel>> GetGallery(int galleryId);
        Task<ResponseBase<IEnumerable<GalleryViewModel>>> GetAllGallery();
        Task<ResponseBase<IEnumerable<GalleryViewModel>>> GetArtistGallery(string userId);
    }
}
