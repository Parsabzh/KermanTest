using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IPeoplePackageService
    {
        Task<ResponseBase<PeoplePackageViewModel>> AddPeoplePackage(PeoplePackageViewModel peoplePackage);
        Task<ResponseBase<PeoplePackageViewModel>> UpdatePeoplePackage(PeoplePackageViewModel peoplePackage);
        Task<ResponseBase<PeoplePackageViewModel>> DeletePeoplePackage(int peoplePackageId);
        Task<ResponseBase<PeoplePackageViewModel>> GetPeoplePackage(int peoplePackageId);
        Task<ResponseBase<PeoplePackageViewModel>> GetPeoplePackage(string userId);
        Task<ResponseBase<IEnumerable<PeoplePackageViewModel>>> GetAllPeoplePackage();
    }
}
