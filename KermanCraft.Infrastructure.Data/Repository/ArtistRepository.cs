using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly KermanCraftDb _db;
        private readonly UserManager<People> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public ArtistRepository(KermanCraftDb db, UserManager<People> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ResponseBase<People>> AddArtistUser(People people, string password)
        {
            try
            {
                people.PhoneNumberConfirmed = true;
                people.TwoFactorEnabled = false;
                if (await _db.Users.AnyAsync(i => i.NationalId == people.NationalId))
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.AlreadyExist, PeopleResource.NationalId)
                    };
                }

                var result = await _userManager.CreateAsync(people, password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Artist"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Artist"));
                    }

                    var response = await _userManager.AddToRoleAsync(people, "Artist");
                    if (response.Succeeded)
                    {
                        return new ResponseBase<People>()
                        {
                            Message = MessageResource.InsertSuccess,
                            IsSuccessful = true,
                            Entity = people
                        };
                    }

                    var roleErrorList = response.Errors.Select(responseError => responseError.Description).ToList();
                    return new ResponseBase<People>()
                    {
                        MessageList = roleErrorList,
                        IsSuccessful = false
                    };
                }

                var errorList = result.Errors.Select(identityError => identityError.Description).ToList();

                return new ResponseBase<People>()
                {
                    MessageList = errorList,
                    IsSuccessful = false,
                    Entity = people
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<People>> UpdateArtistUser(People people)
        {
            try
            {

                if (people == null)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer)
                    };
                }

                if (await _db.Users.AnyAsync(i => i.NationalId == people.NationalId && i.Id != people.Id))
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.AlreadyExist, PeopleResource.NationalId)
                    };
                }

                if (await _db.Users.AnyAsync(u => u.PhoneNumber == people.PhoneNumber && u.Id != people.Id))
                {
                    return new ResponseBase<People>()
                    {
                        Message = string.Format(MessageResource.AlreadyExist, PeopleResource.Mobile),
                        IsSuccessful = false
                    };
                }

                if (await _db.Users.AnyAsync(u => u.Email == people.Email && u.Id != people.Id))
                {
                    return new ResponseBase<People>()
                    {
                        Message = string.Format(MessageResource.AlreadyExist, PeopleResource.Email),
                        IsSuccessful = false
                    };
                }

                var model = await _userManager.FindByIdAsync(people.Id);
                if (model == null)
                    return new ResponseBase<People>()
                    {

                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer),
                        IsSuccessful = false
                    };

                _mapper.Map(people, model);
                await _userManager.UpdateNormalizedUserNameAsync(model);
                var response = await _userManager.UpdateAsync(model);

                if (response.Succeeded)
                {
                    return new ResponseBase<People>()
                    {
                        Entity = people,
                        Message = MessageResource.UpdateSuccess,
                        IsSuccessful = true,
                    };
                }

                var errorList = response.Errors.Select(identityError => identityError.Description).ToList();
                return new ResponseBase<People>()
                {
                    MessageList = errorList,
                    IsSuccessful = false,
                    Entity = people
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<People>> DeleteArtistUser(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer)
                    };
                }

                var model = await _userManager.FindByIdAsync(id);
                if (model == null)
                    return new ResponseBase<People>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer),
                        IsSuccessful = false
                    };
                var result = await _userManager.DeleteAsync(model);
                if (result.Succeeded)
                {
                    return new ResponseBase<People>()
                    {
                        IsSuccessful = true,
                        Message = MessageResource.DeletSuccess
                    };
                }

                var errorList = result.Errors.Select(identityError => identityError.Description).ToList();
                return new ResponseBase<People>()
                {
                    IsSuccessful = false,
                    MessageList = errorList
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<People>>> GetArtistUserList()
        {
            try
            {
                var user = await _userManager.GetUsersInRoleAsync("Artist");
                if (user == null)
                {
                    return new ResponseBase<IEnumerable<People>>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<People>>()
                {
                    Entity = user.ToList(),
                    IsSuccessful = true,
                    Message = MessageResource.SearchSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<People>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<People>> GetArtistUserById(string id)
        {
            try
            {
                var model = await _userManager.FindByIdAsync(id);
                if (model == null)
                    return new ResponseBase<People>()
                    {
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Customer),
                        IsSuccessful = false
                    };
                return new ResponseBase<People>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<People>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<bool>> IsArtistUserInRole(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new ResponseBase<bool>()
                    {
                        Entity = false,
                        Message = string.Format(MessageResource.NotFound, PeopleResource.Mobile),
                        IsSuccessful = false
                    };
                }

                var isInRole = await _userManager.IsInRoleAsync(user, "Artist");
                if (isInRole)
                {
                    return new ResponseBase<bool>()
                    {
                        Entity = true,
                        Message = MessageResource.SearchSuccess,
                        IsSuccessful = true
                    };
                }

                return new ResponseBase<bool>()
                {
                    Message = MessageResource.AccessDenied,
                    IsSuccessful = false,
                    Entity = false
                };

            }
            catch (Exception e)
            {
                return new ResponseBase<bool>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

     
    }

}
