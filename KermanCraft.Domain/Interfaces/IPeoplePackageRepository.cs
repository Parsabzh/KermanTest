using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IPeoplePackageRepository
    {
        Task<ResponseBase<PeoplePackage>> AddPeoplePackage(PeoplePackage peoplePackage);
        Task<ResponseBase<PeoplePackage>> UpdatePeoplePackage(PeoplePackage peoplePackage);
        Task<ResponseBase<PeoplePackage>> DeletePeoplePackage(int peoplePackageId);
        Task<ResponseBase<PeoplePackage>> GetPeoplePackage(int peoplePackageId);
        Task<ResponseBase<PeoplePackage>> GetPeoplePackage(string userId);
        Task<ResponseBase<IEnumerable<PeoplePackage>>> GetAllPeoplePackage();
    }
}
