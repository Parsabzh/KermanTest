using System.Collections.Generic;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Customer;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseBase<AddCustomerViewModel>> AddCustomer(AddCustomerViewModel user);
        Task<ResponseBase<UpdateCustomerViewModel>> UpdateCustomer(UpdateCustomerViewModel user);
        Task DeleteCustomer(string id);
        Task<ResponseBase<IEnumerable<CustomerListViewModel>>> CustomerList();
        Task<ResponseBase<bool>> CustomerIsInRole(string userName);
        Task<ResponseBase<CustomerViewModel>> GetCustomerById(string userId);
        Task<ResponseBase<CustomerViewModel>> GetCustomerByPhone(string phoneNumber);
    }
}
