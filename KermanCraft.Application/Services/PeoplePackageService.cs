using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Package;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class PeoplePackageService:IPeoplePackageService
    {
        private readonly IPeoplePackageRepository _peoplePackageRepository;
        private readonly IMapper _mapper;

        public PeoplePackageService(IMapper mapper, IPeoplePackageRepository peoplePackageRepository)
        {
            _mapper = mapper;
            _peoplePackageRepository = peoplePackageRepository;
        }

        public async Task<ResponseBase<PeoplePackageViewModel>> AddPeoplePackage(PeoplePackageViewModel peoplePackage)
        {
         
            var result = await _peoplePackageRepository.AddPeoplePackage(_mapper.Map<PeoplePackage>(peoplePackage));
            return new ResponseBase<PeoplePackageViewModel>()
            {
                Entity = _mapper.Map<PeoplePackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<PeoplePackageViewModel>> UpdatePeoplePackage(PeoplePackageViewModel peoplePackage)
        {
          
            var result = await _peoplePackageRepository.UpdatePeoplePackage(_mapper.Map<PeoplePackage>(peoplePackage));
            return new ResponseBase<PeoplePackageViewModel>()
            {
                Entity = _mapper.Map<PeoplePackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<PeoplePackageViewModel>> DeletePeoplePackage(int peoplePackageId)
        {
            var result = await _peoplePackageRepository.DeletePeoplePackage(peoplePackageId);
            return new ResponseBase<PeoplePackageViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<PeoplePackageViewModel>> GetPeoplePackage(int peoplePackageId)
        {
            var result = await _peoplePackageRepository.GetPeoplePackage(peoplePackageId);
            return new ResponseBase<PeoplePackageViewModel>()
            {
                Entity = _mapper.Map<PeoplePackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<PeoplePackageViewModel>> GetPeoplePackage(string userId)
        {
            var result = await _peoplePackageRepository.GetPeoplePackage(userId);
            return new ResponseBase<PeoplePackageViewModel>()
            {
                Entity = _mapper.Map<PeoplePackageViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<PeoplePackageViewModel>>> GetAllPeoplePackage()
        {

            var result = await _peoplePackageRepository.GetAllPeoplePackage();
            return new ResponseBase<IEnumerable<PeoplePackageViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<PeoplePackageViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
