using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<ResponseBase<People>> AddCustomer(People people, string password);
        Task<ResponseBase<People>> UpdateCustomer(People people);
        Task<ResponseBase<People>> DeleteCustomer(string id);
        Task<ResponseBase<IEnumerable<People>>> GetCustomerList();
        Task<ResponseBase<People>> GetCustomerById(string id);
        Task<ResponseBase<People>> GetCustomerByMobile(string phoneNumber);
        //bool VerifyEmail(string email);
        //bool VerifyUserName(string userName);

        Task<ResponseBase<bool>> IsCustomerInRole(string userName);
    }
}
