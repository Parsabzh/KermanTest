using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseBase<People>> ChangeArtistPassword(string userId, string pass, string currentPass);
        Task<ResponseBase<People>> ResetArtistPassword(string userId, string pass);
    }
}
