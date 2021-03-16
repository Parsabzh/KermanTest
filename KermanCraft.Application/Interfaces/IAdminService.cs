using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Admin;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
   public  interface IAdminService
    {
        Task<ResponseBase<AddAdminViewModel>> AddAdmin(AddAdminViewModel user);
        Task<ResponseBase<UpdateAdminViewModel>> UpdateAdmin(UpdateAdminViewModel user);
        Task Delete(string id);
        Task<ResponseBase<IEnumerable<AdminListViewModel>>> UserList();
        Task<ResponseBase<bool>> IsInRole(string userName);
        Task<ResponseBase<AdminViewModel>> GetAdminUserById(string userId);

    }
}
