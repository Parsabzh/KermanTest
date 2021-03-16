using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
   public interface IArtistRepository
    {
        Task<ResponseBase<People>> AddArtistUser(People people, string password);
        Task<ResponseBase<People>> UpdateArtistUser(People people);
        Task<ResponseBase<People>> DeleteArtistUser(string id);
        Task<ResponseBase<IEnumerable<People>>> GetArtistUserList();
        Task<ResponseBase<People>> GetArtistUserById(string id);
        //bool VerifyEmail(string email);
        //bool VerifyUserName(string userName);

        Task<ResponseBase<bool>> IsArtistUserInRole(string userName);
     

    }
}
