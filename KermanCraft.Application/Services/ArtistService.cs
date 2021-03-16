using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Artist;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.PeopleResource;

namespace KermanCraft.Application.Services
{
   public class ArtistService:IArtistService
   {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<AddArtistViewModel>> AddArtist(AddArtistViewModel user)
        {

            var model = new People()
            {
                Birthday = PersianDate.ToGeorgianDate(user.Birthday),
                Status = user.Status,
                PhoneNumber = user.PhoneNumber,
                Family = user.Family,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Address = user.Address,
                NationalId = user.NationalId,
                ImageUrl = user.ImageUrl,
                Job = user.Job
            };
            var result = await _artistRepository.AddArtistUser(model, user.Password);
            if (result.IsSuccessful)
            {
                return new ResponseBase<AddArtistViewModel>()
                {
                    Entity = _mapper.Map<AddArtistViewModel>(result.Entity),
                    Message = result.Message,
                    IsSuccessful = result.IsSuccessful
                };
            }
            else
            {
                if (result.MessageList!=null)
                {
                    return new ResponseBase<AddArtistViewModel>()
                    {
                        Entity = _mapper.Map<AddArtistViewModel>(result.Entity),
                        MessageList = result.MessageList,
                        IsSuccessful = false
                    };
                }
                else
                {
                    return new ResponseBase<AddArtistViewModel>()
                    {
                        Entity = _mapper.Map<AddArtistViewModel>(result.Entity),
                        Message = result.Message,
                        IsSuccessful = false
                    };
                }
            }
        }

        public async Task<ResponseBase<UpdateArtistViewModel>> UpdateArtist(UpdateArtistViewModel user)
        {
            var model = new People()
            {
                Birthday = PersianDate.ToGeorgianDate(user.Birthday),
                Status = user.Status,
                PhoneNumber = user.PhoneNumber,
                Family = user.Family,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Address = user.Address,
                NationalId = user.NationalId,
                ImageUrl = user.ImageUrl,
                Id = user.Id,
                Job = user.Job
            };
            var result = await _artistRepository.UpdateArtistUser(model);
            if (result.IsSuccessful)
            {
                return new ResponseBase<UpdateArtistViewModel>()
                {
                    Entity = _mapper.Map<UpdateArtistViewModel>(result.Entity),
                    Message = result.Message,
                    IsSuccessful = result.IsSuccessful
                };
            }
            else
            {
                if (result.MessageList != null)
                {
                    return new ResponseBase<UpdateArtistViewModel>()
                    {
                        Entity = _mapper.Map<UpdateArtistViewModel>(result.Entity),
                        MessageList = result.MessageList,
                        IsSuccessful = false
                    };
                }
                else
                {
                    return new ResponseBase<UpdateArtistViewModel>()
                    {
                        Entity = _mapper.Map<UpdateArtistViewModel>(result.Entity),
                        Message = result.Message,
                        IsSuccessful = false
                    };
                }
            }
        }

        public async Task DeleteArtist(string id)
        {
            await _artistRepository.DeleteArtistUser(id);
        }

       

        public async Task<ResponseBase<IEnumerable<ArtistListViewModel>>> ArtistUserList()
        {
            var result = await _artistRepository.GetArtistUserList();
            if (!result.IsSuccessful)
            {
                return new ResponseBase<IEnumerable<ArtistListViewModel>>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var userList = _mapper.Map<IEnumerable<ArtistListViewModel>>(result.Entity);
            return new ResponseBase<IEnumerable<ArtistListViewModel>>()
            {
                IsSuccessful = true,
                Entity = userList,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<bool>> ArtistIsInRole(string userName)
        {
            var result = await _artistRepository.IsArtistUserInRole(userName);
            return new ResponseBase<bool>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message,
                Entity = result.Entity
            };
        }

        public async Task<ResponseBase<ArtistViewModel>> GetArtistUserById(string userId)
        {
            var result = await _artistRepository.GetArtistUserById(userId);
            if (!result.IsSuccessful)
            {
                return new ResponseBase<ArtistViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var user = new ArtistViewModel()
            {
                Id = result.Entity.Id,
                Family = result.Entity.Family,
                Name = result.Entity.Name,
                PhoneNumber = result.Entity.PhoneNumber,
                Status = result.Entity.Status,
                Email = result.Entity.Email,
                Birthday = result.Entity.Birthday.ToLongDateString(),
                NationalId = result.Entity.NationalId,
                Address = result.Entity.Address,
                ImageUrl = result.Entity.ImageUrl
            };
            return new ResponseBase<ArtistViewModel>()
            {
                Entity = user,
                Message = result.Message,
                IsSuccessful = true
            };
        }

        public ResponseBase<ArtistViewModel> GetArtistUserByMobile(string mobile)
        {
            var artistUser= new People();
            if (_artistRepository != null)
            {
                artistUser = _artistRepository.GetArtistUserList().Result.Entity
                    .SingleOrDefault(i => i.PhoneNumber == mobile);
            }

            if (artistUser==null)
            {
                return new ResponseBase<ArtistViewModel>()
                {
                    IsSuccessful = false,
                    Message = string.Format(MessageResource.NotFound,PeopleResource.User)
                };
            }

            var user=_mapper.Map<ArtistViewModel>(artistUser);
            return new ResponseBase<ArtistViewModel>()
            {
                Entity = user,
                IsSuccessful = true,
                Message = MessageResource.SearchSuccess
            };
        }
   }
}
