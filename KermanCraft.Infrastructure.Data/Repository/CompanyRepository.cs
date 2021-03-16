using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Company;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public CompanyRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ResponseBase<Company>> AddCompany(Company company)
        {
            try
            {
                if (await _db.Companies.AnyAsync(i=>i.Name==company.Name))
                {
                    return new ResponseBase<Company>()
                    {
                        Entity = company,
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.AlreadyExist,"مرکز")
                    };
                }
                await _db.Companies.AddAsync(company);
                await _db.SaveChangesAsync();
                return new ResponseBase<Company>()
                {
                    Entity = company,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Company>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<Company>> UpdateCompany(Company company)
        {
            try
            {
                var model = await _db.Companies.FindAsync(company.CompanyId);
                if (model == null)
                {
                    return new ResponseBase<Company>()
                    {
                        Message = string.Format(MessageResource.NotFound,"مرکز"),
                        IsSuccessful = false
                    };
                }

                _mapper.Map(company, model);
                _db.Companies.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Company>()
                {
                    Entity = company,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Company>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Company>> DeleteCompany(int companyId)
        {
            try
            {
                var model = await _db.Companies.FindAsync(companyId);
                if (model == null)
                {
                    return new ResponseBase<Company>()
                    {
                        Message = string.Format(MessageResource.NotFound, "مرکز"),
                        IsSuccessful = false
                    };
                }

                _db.Companies.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Company>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Company>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<Company>> GetCompany(int companyId)
        {
            try
            {
                var model = await _db.Companies.FindAsync(companyId);
                if (model == null)
                {
                    return new ResponseBase<Company>()
                    {
                        Message = string.Format(MessageResource.NotFound, "مرکز"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<Company>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Company>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Company>>> GetAllCompany()
        {
            try
            {
                var model = await _db.Companies.ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Company>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "مرکز"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Company>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Company>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Company>>> GetUserCompany(string userId)
        {
            try
            {
                var model = await _db.Companies.Where(i=>i.PeopleId==userId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Company>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "مرکز"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Company>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Company>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Company>>> GetCompanyByType(string type)
        {
            try
            {
                var model = await _db.Companies.Where(i => i.Type == type).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<Company>>()
                    {
                        Message = string.Format(MessageResource.NotFound, "مرکز"),
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<Company>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Company>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
