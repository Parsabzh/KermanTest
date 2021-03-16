using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.ProductResource;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly KermanCraftDb _db;
        private readonly IMapper _mapper;

        public ProductRepository(KermanCraftDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseBase<Product>> AddProduct(Product product)
        {
            try
            {

                
                product.Code = $"GHK-1";
                while (await _db.Products.AnyAsync(p => p.Code == product.Code))
                {
                    var rnd = new Random();
                    var code = rnd.Next(1, 50000);
                    product.Code = $"GHK-{code}";
                }
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                return new ResponseBase<Product>()
                {
                    Message = MessageResource.InsertSuccess,
                    IsSuccessful = true,
                    Entity = product
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Product>> UpdateProduct(Product product)
        {
            try
            {
                var model = await _db.Products.FindAsync(product.ProductId);
                if (model == null)
                {
                    return new ResponseBase<Product>()
                    {
                        Message = string.Format(MessageResource.NotFound, ProductResource.Name),
                        IsSuccessful = false
                    };
                }

                _mapper.Map(product, model);
                _db.Products.Update(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Product>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.UpdateSuccess,
                    Entity = product
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Product>> Delete(int id)
        {
            try
            {
                var model = await _db.Products.FindAsync(id);
                if (model == null)
                {
                    return new ResponseBase<Product>()
                    {
                        Message = string.Format(MessageResource.NotFound, ProductResource.Name),
                        IsSuccessful = false
                    };
                }

                _db.Products.Remove(model);
                await _db.SaveChangesAsync();
                return new ResponseBase<Product>()
                {
                    IsSuccessful = true,
                    Message = MessageResource.DeletSuccess
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Product>> GetProduct(int id)
        {
            try
            {
                var model = await _db.Products.FindAsync(id);
                if (model == null)
                {
                    return new ResponseBase<Product>()
                    {
                        Message = string.Format(MessageResource.NotFound, ProductResource.Name),
                        IsSuccessful = false
                    };
                }
                return new ResponseBase<Product>()
                {
                    Entity = model,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Product>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Product>>> ProductsList()
        {
            try
            {
                var products = await _db.Products.ToListAsync();
                if (products == null)
                {
                    return new ResponseBase<IEnumerable<Product>>()
                    {
                        Message = string.Format(MessageResource.NotFound, ProductResource.Name),
                        IsSuccessful = false
                    };
                }
                return new ResponseBase<IEnumerable<Product>>()
                {
                    Entity = products,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Product>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Product>>> GetArtistProduct(string userId)
        {
            try
            {
                var model = await _db.Products.Where(i => i.PeopleId == userId).ToListAsync();
                if (model.Any())
                {
                    return new ResponseBase<IEnumerable<Product>>()
                    {
                        Entity = model,
                        IsSuccessful = true,
                        Message = MessageResource.SearchSuccess
                    };
                }
                return new ResponseBase<IEnumerable<Product>>()
                {
                  
                    IsSuccessful = false,
                    Message = string.Format(MessageResource.NotFound,ProductResource.Name)
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Product>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<IEnumerable<Product>>> GetProductByType(string type)
        {
            try
            {
                var model = await _db.Products.Where(i => i.Type == type).ToListAsync();
                if (model.Any())
                {
                    return new ResponseBase<IEnumerable<Product>>()
                    {
                        Entity = model,
                        IsSuccessful = true,
                        Message = MessageResource.SearchSuccess
                    };
                }
                return new ResponseBase<IEnumerable<Product>>()
                {

                    IsSuccessful = false,
                    Message = string.Format(MessageResource.NotFound, ProductResource.Name)
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<IEnumerable<Product>>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }
    }
}
