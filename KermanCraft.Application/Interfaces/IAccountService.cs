using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseBase<People>> ChangeArtistPassword(ChangePasswordViewModel pass);
        Task<ResponseBase<People>> ResetArtistPassword(string userId, string pass);
    }
}
