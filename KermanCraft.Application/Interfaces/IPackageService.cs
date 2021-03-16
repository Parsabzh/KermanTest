using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IPackageService
    {
        Task<ResponseBase<PackageViewModel>> AddPackage(PackageViewModel package);
        Task<ResponseBase<PackageViewModel>> UpdatePackage(PackageViewModel package);
        Task<ResponseBase<PackageViewModel>> DeletePackage(int packageId);
        Task<ResponseBase<PackageViewModel>> GetPackage(int packageId);
        Task<ResponseBase<IEnumerable<PackageViewModel>>> GetAllPackage();
    }
}
