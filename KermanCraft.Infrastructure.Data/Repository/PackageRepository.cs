using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public PackageRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Package>> AddPackage(Package package)
        {
            try
            {
                await _db.Packages.AddAsync(package);
                await _db.SaveChangesAsync();
                return new ResponseBase<Package>()
                {
                    Entity = package,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Package>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
  

    public async Task<ResponseBase<Package>> UpdatePackage(Package package)
    {
        try
        {
            var model = await _db.Packages.FindAsync(package.PackageId);
            if (model == null)
            {
                return new ResponseBase<Package>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _mapper.Map(package, model);
            _db.Packages.Update(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<Package>()
            {
                Entity = package,
                IsSuccessful = true,
                Message = MessageResource.UpdateSuccess
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<Package>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<Package>> DeletePackage(int packageId)
    {
        try
        {
            var model = await _db.Packages.FindAsync(packageId);
            if (model == null)
            {
                return new ResponseBase<Package>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _db.Packages.Remove(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<Package>()
            {
                Message = MessageResource.DeletSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<Package>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<Package>> GetPackage(int packageId)
    {
        try
        {
            var model = await _db.Packages.FindAsync(packageId);
            if (model == null)
            {
                return new ResponseBase<Package>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<Package>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<Package>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<IEnumerable<Package>>> GetAllPackage()
    {
        try
        {
            var model = await _db.Packages.ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<Package>>()
                {
                    Message = "پکیج موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<Package>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<Package>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
}
}
