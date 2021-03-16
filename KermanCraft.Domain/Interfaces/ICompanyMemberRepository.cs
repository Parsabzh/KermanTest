using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface ICompanyMemberRepository
    {
        Task<ResponseBase<CompanyMember>> AddCompanyMember(CompanyMember companyMember);
        Task<ResponseBase<CompanyMember>> UpdateCompanyMember(CompanyMember companyMember);
        Task<ResponseBase<CompanyMember>> DeleteCompanyMember(int companyMemberId);
        Task<ResponseBase<CompanyMember>> GetCompanyMember(int companyMemberId);
        Task<ResponseBase<IEnumerable<CompanyMember>>> GetCompanyMembers(int companyId);

        Task<ResponseBase<IEnumerable<CompanyMember>>> GetAllCompanyMember();

    }
}
