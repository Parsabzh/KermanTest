using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<ResponseBase<People>> AddAdminUser(People people, string password);
        Task<ResponseBase<People>> UpdateAdminUser(People people);
        Task<ResponseBase<People>> DeleteAdminUser(string id);
        Task<ResponseBase<IEnumerable<People>>> GetAdminUserList();
        Task<ResponseBase<People>> GetAdminUserById(string id);
        //bool VerifyEmail(string email);
        //bool VerifyUserName(string userName);
        
        Task<ResponseBase<bool>> IsAdminUserInRole(string userName);
    }
}
