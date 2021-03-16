using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface ICompanyMemberService
    {
        Task<ResponseBase<CompanyMemberViewModel>> AddCompanyMember(CompanyMemberViewModel companyMember);
        Task<ResponseBase<CompanyMemberViewModel>> UpdateCompanyMember(CompanyMemberViewModel companyMember);
        Task<ResponseBase<CompanyMemberViewModel>> DeleteCompanyMember(int companyMemberId);
        Task<ResponseBase<CompanyMemberViewModel>> GetCompanyMember(int companyMemberId);
        Task<ResponseBase<IEnumerable<CompanyMemberViewModel>>> GetAllCompanyMember();
        Task<ResponseBase<IEnumerable<CompanyMemberViewModel>>> GetCompanyMembers(int companyId);
    }
}
