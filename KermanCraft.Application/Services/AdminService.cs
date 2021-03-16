using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Admin;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<AddAdminViewModel>> AddAdmin(AddAdminViewModel user)
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
                ImageUrl = user.ImageUrl
            };
            var result = await _adminRepository.AddAdminUser(model, user.Password);
            return result.IsSuccessful switch
            {
                true => new ResponseBase<AddAdminViewModel>()
                {
                    IsSuccessful = true,
                    Message = result.Message, 
                    Entity = _mapper.Map<AddAdminViewModel>(result.Entity)
                },
                false when result.MessageList != null => new ResponseBase<AddAdminViewModel>()
                {
                    IsSuccessful = false, 
                    MessageList = result.MessageList,
                    Entity = _mapper.Map<AddAdminViewModel>(result.Entity)
                },
                false when result.Message !=null => new ResponseBase<AddAdminViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = _mapper.Map<AddAdminViewModel>(result.Entity)
                }
                ,
                _ => new ResponseBase<AddAdminViewModel>()
                {
                    IsSuccessful = false, Message = result.Message, Entity = user
                }
            };
        }

        public async Task<ResponseBase<UpdateAdminViewModel>> UpdateAdmin(UpdateAdminViewModel user)
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
                Id = user.Id
            };
            var result = await _adminRepository.UpdateAdminUser(model);
            return result.IsSuccessful switch
            {
                true => new ResponseBase<UpdateAdminViewModel>()
                {
                    IsSuccessful = true, 
                    Message = result.Message,
                    Entity = _mapper.Map<UpdateAdminViewModel>(result.Entity)
                },
                false when result.MessageList != null => new ResponseBase<UpdateAdminViewModel>()
                {
                    IsSuccessful = false, 
                    MessageList = result.MessageList,
                    Entity = _mapper.Map<UpdateAdminViewModel>(result.Entity)
                },
                _ => new ResponseBase<UpdateAdminViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = _mapper.Map<UpdateAdminViewModel>(result.Entity)
                }
            };
        }

        public async Task Delete(string id)
        {
            await _adminRepository.DeleteAdminUser(id);
        }

        public async Task<ResponseBase<IEnumerable<AdminListViewModel>>> UserList()
        {
            var result = await _adminRepository.GetAdminUserList();
            if (!result.IsSuccessful)
            {
                return new ResponseBase<IEnumerable<AdminListViewModel>>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var userList = _mapper.Map<IEnumerable<AdminListViewModel>>(result.Entity);
            return new ResponseBase<IEnumerable<AdminListViewModel>>()
            {
                IsSuccessful = true,
                Entity = userList,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<AdminViewModel>> GetAdminUserById(string userId)
        {
            var result = await _adminRepository.GetAdminUserById(userId);
            if (!result.IsSuccessful)
            {
                return new ResponseBase<AdminViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var user = new AdminViewModel()
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
            return new ResponseBase<AdminViewModel>()
            {
                Entity = user,
                Message = result.Message,
                IsSuccessful = true
            };
        }

        public async Task<ResponseBase<bool>> IsInRole(string userName)
        {
            var result = await _adminRepository.IsAdminUserInRole(userName);
            return new ResponseBase<bool>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message,
                Entity = result.Entity
            };
        }
    }
}
