using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.ArtGallery;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
   public interface IArtGalleryRepository
    {
        Task<ResponseBase<ArtGallery>> AddArtGallery(ArtGallery artGallery);
        Task<ResponseBase<ArtGallery>> UpdateArtGallery(ArtGallery artGallery);
        Task<ResponseBase<ArtGallery>> DeleteArtGallery(int artGalleryId);
        Task<ResponseBase<ArtGallery>> GetArtGallery(int artGalleryId);
        Task<ResponseBase<IEnumerable<ArtGallery>>> GetAllArtGallery();

    }
}
