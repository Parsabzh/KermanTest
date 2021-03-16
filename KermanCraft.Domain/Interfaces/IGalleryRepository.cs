using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Gallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IGalleryRepository
    {
        Task<ResponseBase<Gallery>> AddGallery(Gallery gallery);
        Task<ResponseBase<Gallery>> UpdateGallery(Gallery gallery);
        Task<ResponseBase<Gallery>> DeleteGallery(int galleryId);
        Task<ResponseBase<Gallery>> GetGallery(int galleryId);
        Task<ResponseBase<IEnumerable<Gallery>>> GetAllGallery();
        Task<ResponseBase<IEnumerable<Gallery>>> GetArtistGallery(string userId);

    }
}
