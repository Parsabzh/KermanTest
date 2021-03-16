using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Sms;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Sms;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
   public  class SmsService:ISmsService
   {
       private readonly ISmsRepository _smsRepository;
       private readonly IMapper _mapper;

       public SmsService(ISmsRepository smsRepository, IMapper mapper)
       {
           _smsRepository = smsRepository;
           _mapper = mapper;
       }

       public async Task<ResponseBase<SmsViewModel>> AddSms(string userPhone)
        {
            var result = await _smsRepository.AddSms(userPhone);
            return new ResponseBase<SmsViewModel>()
            {
                Entity = _mapper.Map<SmsViewModel>(result.Entity),
                Message = result.Message,
                IsSuccessful = result.IsSuccessful
            };
        }

       public async Task<ResponseBase<SmsViewModel>> GetCode(string userPhone)
       {
           var result = await _smsRepository.GetCode(userPhone);
           return new ResponseBase<SmsViewModel>()
           {
               Entity = _mapper.Map<SmsViewModel>(result.Entity),
               Message = result.Message,
               IsSuccessful = result.IsSuccessful
           };
        }

       public async Task<ResponseBase<SmsViewModel>> DeleteCode(string userPhone)
       {
           var result = await _smsRepository.DeleteCode(userPhone);
           return new ResponseBase<SmsViewModel>()
           {
               Entity = _mapper.Map<SmsViewModel>(result.Entity),
               Message = result.Message,
               IsSuccessful = result.IsSuccessful
           };
        }
   }
}
