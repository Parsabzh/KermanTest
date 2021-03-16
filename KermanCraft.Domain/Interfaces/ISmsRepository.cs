using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Sms;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ISmsRepository
    {
        Task<ResponseBase<Sms>> AddSms(string userPhone);
        Task<ResponseBase<Sms>> GetCode(string userPhone);
        Task<ResponseBase<Sms>> DeleteCode(string userPhone);

    }
}
