using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.Models.People;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class PeoplePackageRepository : IPeoplePackageRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;
        private readonly UserManager<People> _userManager;

        public PeoplePackageRepository(KermanCraftDb db, IMapper mapper, UserManager<People> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseBase<PeoplePackage>> AddPeoplePackage(PeoplePackage peoplePackage)
        {
            try
            {
                if (await _db.PeoplePackages.AnyAsync(i=>i.PeopleId==peoplePackage.PeopleId && i.Status==true))
                {
                    return new ResponseBase<PeoplePackage>()
                    {
                        IsSuccessful = false,
                        Message = "شما دارای پکیج فعال هستید"
                    };
                }
                await _db.PeoplePackages.AddAsync(peoplePackage);
                await _db.SaveChangesAsync();
                return new ResponseBase<PeoplePackage>()
                {
                    Entity = peoplePackage,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<PeoplePackage>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
  

    public async Task<ResponseBase<PeoplePackage>> UpdatePeoplePackage(PeoplePackage peoplePackage)
    {
        try
        {
            var model = await _db.PeoplePackages.FindAsync(peoplePackage.PeoplePackageId);
            if (model == null)
            {
                return new ResponseBase<PeoplePackage>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _mapper.Map(peoplePackage, model);
            _db.PeoplePackages.Update(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<PeoplePackage>()
            {
                Entity = peoplePackage,
                IsSuccessful = true,
                Message = MessageResource.UpdateSuccess
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<PeoplePackage>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<PeoplePackage>> DeletePeoplePackage(int peoplePackageId)
    {
        try
        {
            var model = await _db.PeoplePackages.FindAsync(peoplePackageId);
            if (model == null)
            {
                return new ResponseBase<PeoplePackage>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _db.PeoplePackages.Remove(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<PeoplePackage>()
            {
                Message = MessageResource.DeletSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<PeoplePackage>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<PeoplePackage>> GetPeoplePackage(int peoplePackageId)
    {
        try
        {
            var model = await _db.PeoplePackages.FindAsync(peoplePackageId);
            if (model == null)
            {
                return new ResponseBase<PeoplePackage>()
                {
                    Entity = null,
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<PeoplePackage>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<PeoplePackage>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
    public async Task<ResponseBase<PeoplePackage>> GetPeoplePackage(string userId)
    {
        try
        {
            var model = await _db.PeoplePackages.SingleOrDefaultAsync(i=>i.PeopleId==userId);
            if (model == null)
            {
                return new ResponseBase<PeoplePackage>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<PeoplePackage>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<PeoplePackage>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
        public async Task<ResponseBase<IEnumerable<PeoplePackage>>> GetAllPeoplePackage()
    {
        try
        {
            var model = await _db.PeoplePackages.ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<PeoplePackage>>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<PeoplePackage>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<PeoplePackage>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
}
}
