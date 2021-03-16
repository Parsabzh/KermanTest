using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.ArtGallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IArtGalleryService
    {
        Task<ResponseBase<ArtGalleryViewModel>> AddArtGallery(ArtGalleryViewModel artGallery);
        Task<ResponseBase<ArtGalleryViewModel>> UpdateArtGallery(ArtGalleryViewModel artGallery);
        Task<ResponseBase<ArtGalleryViewModel>> DeleteArtGallery(int artGalleryId);
        Task<ResponseBase<ArtGalleryViewModel>> GetArtGallery(int artGalleryId);
        Task<ResponseBase<IEnumerable<ArtGalleryViewModel>>> GetAllArtGallery();
    }
}
