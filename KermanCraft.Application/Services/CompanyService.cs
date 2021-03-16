using System.Collections.Generic;
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
    public class CompanyService:ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<CompanyViewModel>> AddCompany(CompanyViewModel company)
        {
            var model = new Company()
            {
                Description = company.Description,
              
                Name = company.Name,
                PeopleId = company.PeopleId,
                StartDate = PersianDate.ToGeorgianDate(company.StartDate),
                Status = company.Status,
                Type = company.Type,
                Manager = company.Manager,
                RegistrationNum = company.RegistrationNum,
                ImageUrl = company.ImageUrl,
                Address = company.Address,
                Email = company.Email,
                Tel = company.Tel
            };
            var result = await _companyRepository.AddCompany(model);

            var vm = new CompanyViewModel()
            {
                Description = result.Entity.Description,
                Name = result.Entity.Name,
                PeopleId = company.PeopleId,
                StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
                Status = result.Entity.Status,
                Type = company.Type,
                Manager = company.Manager,
                RegistrationNum = company.RegistrationNum,
                ImageUrl = company.ImageUrl,
                Address = company.Address,
                Email = company.Email,
                Tel = company.Tel
            };
            return new ResponseBase<CompanyViewModel>()
            {
                Entity =vm,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<CompanyViewModel>> UpdateCompany(CompanyViewModel company)
        {
            var model = new Company()
            {
                Description = company.Description,
                Name = company.Name,
                PeopleId = company.PeopleId,
                StartDate = PersianDate.ToGeorgianDate(company.StartDate),
                Status = company.Status,
                CompanyId = company.CompanyId,
                Type = company.Type,
                Manager = company.Manager,
                RegistrationNum = company.RegistrationNum,
                ImageUrl = company.ImageUrl,
                Address = company.Address,
                Email = company.Email,
                Tel = company.Tel
            };
            var result = await _companyRepository.UpdateCompany(model);
            var vm = new CompanyViewModel()
            {
                Description = result.Entity.Description,
                Name = result.Entity.Name,
                PeopleId = result.Entity.PeopleId,
                StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
                Status = result.Entity.Status,
                Type = company.Type,
                Manager = company.Manager,
                RegistrationNum = company.RegistrationNum,
                ImageUrl = company.ImageUrl,
                Address = company.Address,
                Email = company.Email,
                Tel = company.Tel
            };
            return new ResponseBase<CompanyViewModel>()
            {
                Entity = vm,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CompanyViewModel>> DeleteCompany(int companyId)
        {
            var result = await _companyRepository.DeleteCompany(companyId);
            return new ResponseBase<CompanyViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<CompanyViewModel>> GetCompany(int companyId)
        {
            var result = await _companyRepository.GetCompany(companyId);
            //var vm = new CompanyViewModel()
            //{
            //    Description = result.Entity.Description,
            //    Name = result.Entity.Name,
            //    PeopleId = result.Entity.PeopleId,
            //    StartDate = PersianDate.ToPersianDateString(result.Entity.StartDate),
            //    Status = result.Entity.Status
            //};

            return new ResponseBase<CompanyViewModel>()
            {
                Entity = _mapper.Map<CompanyViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetAllCompany()
        {
            var result = await _companyRepository.GetAllCompany();
            return new ResponseBase<IEnumerable<CompanyViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CompanyViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetUserCompany(string userId)
        {
            var result = await _companyRepository.GetUserCompany(userId);
            return new ResponseBase<IEnumerable<CompanyViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CompanyViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<CompanyViewModel>>> GetCompanyByType(string type)
        {
            var result = await _companyRepository.GetCompanyByType(type);
            return new ResponseBase<IEnumerable<CompanyViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<CompanyViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
