using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
   public class CustomerService:ICustomerService
   {
        private readonly ICustomerRepository _artistRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _artistRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<AddCustomerViewModel>> AddCustomer(AddCustomerViewModel user)
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
            var result = await _artistRepository.AddCustomer(model, user.Password);
            return result.IsSuccessful switch
            {
                true => new ResponseBase<AddCustomerViewModel>()
                {
                    IsSuccessful = true,
                    Message = result.Message,
                    Entity = _mapper.Map<AddCustomerViewModel>(result.Entity)
                },
                false when result.MessageList != null => new ResponseBase<AddCustomerViewModel>()
                {
                    IsSuccessful = false,
                    MessageList = result.MessageList,
                    Entity = _mapper.Map<AddCustomerViewModel>(result.Entity)
                },
                false when result.Message!=null=> new ResponseBase<AddCustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = _mapper.Map<AddCustomerViewModel>(result.Entity)
                },
                _ => new ResponseBase<AddCustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = _mapper.Map<AddCustomerViewModel>(result.Entity)
                }
            };
        }

        public async Task<ResponseBase<UpdateCustomerViewModel>> UpdateCustomer(UpdateCustomerViewModel user)
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
            var result = await _artistRepository.UpdateCustomer(model);
            return result.IsSuccessful switch
            {
                true => new ResponseBase<UpdateCustomerViewModel>()
                {
                    IsSuccessful = true,
                    Message = result.Message,
                    Entity = _mapper.Map<UpdateCustomerViewModel>(result.Entity)
                },
                false when result.MessageList != null => new ResponseBase<UpdateCustomerViewModel>()
                {
                    IsSuccessful = false,
                    MessageList = result.MessageList,
                    Entity = _mapper.Map<UpdateCustomerViewModel>(result.Entity)
                },
                false when result.Message != null => new ResponseBase<UpdateCustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = _mapper.Map<UpdateCustomerViewModel>(result.Entity)
                },
                _ => new ResponseBase<UpdateCustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message,
                    Entity = user
                }
            };
        }

        public async Task DeleteCustomer(string id)
        {
            await _artistRepository.DeleteCustomer(id);
        }

        public async Task<ResponseBase<IEnumerable<CustomerListViewModel>>> CustomerList()
        {
            var result = await _artistRepository.GetCustomerList();
            if (!result.IsSuccessful)
            {
                return new ResponseBase<IEnumerable<CustomerListViewModel>>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var userList = _mapper.Map<IEnumerable<CustomerListViewModel>>(result.Entity);
            return new ResponseBase<IEnumerable<CustomerListViewModel>>()
            {
                IsSuccessful = true,
                Entity = userList,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<bool>> CustomerIsInRole(string userName)
        {
            var result = await _artistRepository.IsCustomerInRole(userName);
            return new ResponseBase<bool>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message,
                Entity = result.Entity
            };
        }

        public async Task<ResponseBase<CustomerViewModel>> GetCustomerById(string userId)
        {
            var result = await _artistRepository.GetCustomerById(userId);
            if (!result.IsSuccessful)
            {
                return new ResponseBase<CustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var user = new CustomerViewModel()
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
            return new ResponseBase<CustomerViewModel>()
            {
                Entity = user,
                Message = result.Message,
                IsSuccessful = true
            };
        }

        public async Task<ResponseBase<CustomerViewModel>> GetCustomerByPhone(string phoneNumber)
        {
            var result = await _artistRepository.GetCustomerByMobile(phoneNumber);
            if (!result.IsSuccessful)
            {
                return new ResponseBase<CustomerViewModel>()
                {
                    IsSuccessful = false,
                    Message = result.Message
                };
            }

            var user = new CustomerViewModel()
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
            return new ResponseBase<CustomerViewModel>()
            {
                Entity = user,
                Message = result.Message,
                IsSuccessful = true
            };
        }
   }
}
