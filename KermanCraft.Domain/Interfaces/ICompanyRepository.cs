using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
   public interface ICompanyRepository
    {
        Task<ResponseBase<Company>> AddCompany(Company company);
        Task<ResponseBase<Company>> UpdateCompany(Company company);
        Task<ResponseBase<Company>> DeleteCompany(int companyId);
        Task<ResponseBase<Company>> GetCompany(int companyId);
        Task<ResponseBase<IEnumerable<Company>>> GetAllCompany();
        Task<ResponseBase<IEnumerable<Company>>> GetUserCompany(string userId);
        Task<ResponseBase<IEnumerable<Company>>> GetCompanyByType(string type);

    }
}
