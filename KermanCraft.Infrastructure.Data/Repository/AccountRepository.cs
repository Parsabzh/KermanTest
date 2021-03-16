using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Identity;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<People> _userManager;

        public AccountRepository(UserManager<People> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseBase<People>> ChangeArtistPassword(string userId, string newPass, string currentPass)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User)
                    };
                }

                var result = await _userManager.ChangePasswordAsync(user, currentPass, newPass);
                if (result.Succeeded)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = true,
                        Message = MessageResource.UpdateSuccess,
                        Entity = user
                    };
                }

                var errorList = new List<string>(5);
                errorList.AddRange(result.Errors.Select(identityError => identityError.Description));
                return new ResponseBase<People>()
                {
                    Entity = user,
                    IsSuccessful = false,
                    MessageList = errorList
                };

            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<People>> ResetArtistPassword(string userId, string pass)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.User)
                    };
                }
                var errorList = new List<string>(5);
                var removePass = await _userManager.RemovePasswordAsync(user);
                if (!removePass.Succeeded)
                {
                    errorList = new List<string>(5);
                    errorList.AddRange(removePass.Errors.Select(identityError => identityError.Description));
                    return new ResponseBase<People>()
                    {
                        Entity = user,
                        IsSuccessful = false,
                        MessageList = errorList
                    };
                }
                var result = await _userManager.AddPasswordAsync(user, pass);
                if (result.Succeeded)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = true,
                        Message = MessageResource.UpdateSuccess,
                        Entity = user
                    };
                }


                errorList.AddRange(result.Errors.Select(identityError => identityError.Description));
                return new ResponseBase<People>()
                {
                    Entity = user,
                    IsSuccessful = false,
                    MessageList = errorList
                };

            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
