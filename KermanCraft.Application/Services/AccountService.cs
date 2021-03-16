using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Account;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResponseBase<People>> ChangeArtistPassword(ChangePasswordViewModel pass)
        {
            var result = await _accountRepository.ChangeArtistPassword(pass.UserId, pass.NewPass, pass.CurrentPassWord);
            return new ResponseBase<People>()
            {
                Entity=result.Entity,
                MessageList = result.MessageList,
                IsSuccessful = result.IsSuccessful
            };
        }

        public async Task<ResponseBase<People>> ResetArtistPassword(string userId, string pass)
        {
            var result = await _accountRepository.ResetArtistPassword(userId, pass);
            return new ResponseBase<People>()
            {
                Entity = result.Entity,
                MessageList = result.MessageList,
                IsSuccessful = result.IsSuccessful
            };
        }
    }
}
