using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
   public interface ICompanyService
    {
        Task<ResponseBase<CompanyViewModel>> AddCompany(CompanyViewModel company);
        Task<ResponseBase<CompanyViewModel>> UpdateCompany(CompanyViewModel company);
        Task<ResponseBase<CompanyViewModel>> DeleteCompany(int companyId);
        Task<ResponseBase<CompanyViewModel>> GetCompany(int companyId);
        Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetAllCompany();
        Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetUserCompany(string userId);
        Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetCompanyByType(string type);
    }
}
