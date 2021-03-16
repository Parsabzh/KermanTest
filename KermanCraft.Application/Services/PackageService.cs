using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Package;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class PackageService:IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        
        private readonly IMapper _mapper;

        public PackageService(IMapper mapper, IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _packageRepository = packageRepository;
        }

        public async Task<ResponseBase<PackageViewModel>> AddPackage(PackageViewModel package)
        {
          
            var result = await _packageRepository.AddPackage(_mapper.Map<Package>(package));
            return new ResponseBase<PackageViewModel>()
            {
                Entity = _mapper.Map<PackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<PackageViewModel>> UpdatePackage(PackageViewModel package)
        {
          
            var result = await _packageRepository.UpdatePackage(_mapper.Map<Package>(package));
            return new ResponseBase<PackageViewModel>()
            {
                Entity = _mapper.Map<PackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<PackageViewModel>> DeletePackage(int packageId)
        {
            var result = await _packageRepository.DeletePackage(packageId);
            return new ResponseBase<PackageViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<PackageViewModel>> GetPackage(int packageId)
        {
            var result = await _packageRepository.GetPackage(packageId);
            return new ResponseBase<PackageViewModel>()
            {
                Entity = _mapper.Map<PackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<PackageViewModel>>> GetAllPackage()
        {

            var result = await _packageRepository.GetAllPackage();
            return new ResponseBase<IEnumerable<PackageViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<PackageViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
