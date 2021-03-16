using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Company;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class CompanyMemberService:ICompanyMemberService
    {
        private readonly ICompanyMemberRepository _companyMemberRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public CompanyMemberService(IMapper mapper, ICompanyMemberRepository companyMemberRepository, IImageService imageService)
        {
            _mapper = mapper;
            _companyMemberRepository = companyMemberRepository;
            _imageService = imageService;
        }

        public async Task<ResponseBase<CompanyMemberViewModel>> AddCompanyMember(CompanyMemberViewModel companyMember)
        {
            
            var result = await _companyMemberRepository.AddCompanyMember(_mapper.Map<CompanyMember>(companyMember));
            return new ResponseBase<CompanyMemberViewModel>()
            {
                Entity = _mapper.Map<CompanyMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<CompanyMemberViewModel>> UpdateCompanyMember(CompanyMemberViewModel companyMember)
        {
         
            var result = await _companyMemberRepository.UpdateCompanyMember(_mapper.Map<CompanyMember>(companyMember));
            return new ResponseBase<CompanyMemberViewModel>()
            {
                Entity = _mapper.Map<CompanyMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CompanyMemberViewModel>> DeleteCompanyMember(int companyMemberId)
        {
            var result = await _companyMemberRepository.DeleteCompanyMember(companyMemberId);
            return new ResponseBase<CompanyMemberViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CompanyMemberViewModel>> GetCompanyMember(int companyMemberId)
        {
            var result = await _companyMemberRepository.GetCompanyMember(companyMemberId);
            return new ResponseBase<CompanyMemberViewModel>()
            {
                Entity = _mapper.Map<CompanyMemberViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CompanyMemberViewModel>>> GetAllCompanyMember()
        {

            var result = await _companyMemberRepository.GetAllCompanyMember();
            return new ResponseBase<IEnumerable<CompanyMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CompanyMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CompanyMemberViewModel>>> GetCompanyMembers(int companyId)
        {
            var result = await _companyMemberRepository.GetCompanyMembers(companyId);
            return new ResponseBase<IEnumerable<CompanyMemberViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CompanyMemberViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
