using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CompanyMemberRepository : ICompanyMemberRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public CompanyMemberRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<CompanyMember>> AddCompanyMember(CompanyMember companyMember)
        {
            try
            {
                await _db.CompanyMembers.AddAsync(companyMember);
                await _db.SaveChangesAsync();
                return new ResponseBase<CompanyMember>()
                {
                    Entity = companyMember,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<CompanyMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }


        public async Task<ResponseBase<CompanyMember>> UpdateCompanyMember(CompanyMember companyMember)
        {
            try
            {
                var model = await _db.CompanyMembers.FindAsync(companyMember.CompanyMemberId);
                if (model == null)
                {
                    return new ResponseBase<CompanyMember>()
                    {
                        Message = "کاربر موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _mapper.Map(companyMember, model);
                _db.CompanyMembers.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<CompanyMember>()
                {
                    Entity = companyMember,
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<CompanyMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<CompanyMember>> DeleteCompanyMember(int companyMemberId)
        {
            try
            {
                var model = await _db.CompanyMembers.FindAsync(companyMemberId);
                if (model == null)
                {
                    return new ResponseBase<CompanyMember>()
                    {
                        Message = "کاربر موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                _db.CompanyMembers.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<CompanyMember>()
                {
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<CompanyMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<CompanyMember>> GetCompanyMember(int companyMemberId)
        {
            try
            {
                var model = await _db.CompanyMembers.FindAsync(companyMemberId);
                if (model == null)
                {
                    return new ResponseBase<CompanyMember>()
                    {
                        Message = "کاربر موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<CompanyMember>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<CompanyMember>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<CompanyMember>>> GetCompanyMembers(int companyId)
        {
            try
            {
                var model = await _db.CompanyMembers.Where(i => i.CompanyId == companyId).ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<CompanyMember>>()
                    {
                        Message = "کاربر موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<CompanyMember>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<CompanyMember>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<CompanyMember>>> GetAllCompanyMember()
        {
            try
            {
                var model = await _db.CompanyMembers.ToListAsync();
                if (!model.Any())
                {
                    return new ResponseBase<IEnumerable<CompanyMember>>()
                    {
                        Message = "کاربر موردنظر یافت نشد.",
                        IsSuccessful = false
                    };
                }

                return new ResponseBase<IEnumerable<CompanyMember>>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<CompanyMember>>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
    }
}
