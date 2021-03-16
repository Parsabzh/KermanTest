using System.Collections.Generic;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IArtistService
    {
        Task<ResponseBase<AddArtistViewModel>> AddArtist(AddArtistViewModel user);
        Task<ResponseBase<UpdateArtistViewModel>> UpdateArtist(UpdateArtistViewModel user);
        Task DeleteArtist(string id);
        Task<ResponseBase<IEnumerable<ArtistListViewModel>>> ArtistUserList();
        Task<ResponseBase<bool>> ArtistIsInRole(string userName);
        Task<ResponseBase<ArtistViewModel>> GetArtistUserById(string userId);
        ResponseBase<ArtistViewModel> GetArtistUserByMobile(string mobile);

    }
}
