using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Sms;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
   public interface ISmsService
   {
       
       Task<ResponseBase<SmsViewModel>> AddSms(string userPhone);
       Task<ResponseBase<SmsViewModel>> GetCode(string userPhone);
       Task<ResponseBase<SmsViewModel>> DeleteCode(string userPhone);
    }
}
