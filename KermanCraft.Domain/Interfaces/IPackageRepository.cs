using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<ResponseBase<Package>> AddPackage(Package package);
        Task<ResponseBase<Package>> UpdatePackage(Package package);
        Task<ResponseBase<Package>> DeletePackage(int packageId);
        Task<ResponseBase<Package>> GetPackage(int packageId);
        Task<ResponseBase<IEnumerable<Package>>> GetAllPackage();
    }
}
