using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Product;

using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public ProductTypeRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<ProductType>> AddProductType(ProductType productType)
        {
            try
            {
                await _db.ProductTypes.AddAsync(productType);
                await _db.SaveChangesAsync();
                return new ResponseBase<ProductType>()
                {
                    Entity = productType,
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<ProductType>()
                {
                    IsSuccessful = false,
                    Message = e.Message
                };
            }
        }
  

    public async Task<ResponseBase<ProductType>> UpdateProductType(ProductType productType)
    {
        try
        {
            var model = await _db.ProductTypes.FindAsync(productType.ProductTypeId);
            if (model == null)
            {
                return new ResponseBase<ProductType>()
                {
                    Message = "نوع موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _mapper.Map(productType, model);
            _db.ProductTypes.Update(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<ProductType>()
            {
                Entity = productType,
                IsSuccessful = true,
                Message = MessageResource.UpdateSuccess
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<ProductType>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<ProductType>> DeleteProductType(int productTypeId)
    {
        try
        {
            var model = await _db.ProductTypes.FindAsync(productTypeId);
            if (model == null)
            {
                return new ResponseBase<ProductType>()
                {
                    Message = "نوع موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            _db.ProductTypes.Remove(model);
            await _db.SaveChangesAsync();
            return new ResponseBase<ProductType>()
            {
                Message = MessageResource.DeletSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<ProductType>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<ProductType>> GetProductType(int productTypeId)
    {
        try
        {
            var model = await _db.ProductTypes.FindAsync(productTypeId);
            if (model == null)
            {
                return new ResponseBase<ProductType>()
                {
                    Message = "نوع موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<ProductType>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<ProductType>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }

    public async Task<ResponseBase<IEnumerable<ProductType>>> GetAllProductType()
    {
        try
        {
            var model = await _db.ProductTypes.ToListAsync();
            if (model == null)
            {
                return new ResponseBase<IEnumerable<ProductType>>()
                {
                    Message = "نوع موردنظر یافت نشد.",
                    IsSuccessful = false
                };
            }

            return new ResponseBase<IEnumerable<ProductType>>()
            {
                Entity = model,
                Message = MessageResource.SearchSuccess,
                IsSuccessful = true
            };
        }
        catch (Exception e)
        {
            return new ResponseBase<IEnumerable<ProductType>>()
            {
                IsSuccessful = false,
                Message = e.Message
            };
        }
    }
}
}
