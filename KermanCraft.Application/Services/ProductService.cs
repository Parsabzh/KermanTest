using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase<AddUpdateProductViewModel>> AddProduct(AddUpdateProductViewModel product)
        {
            var model = _mapper.Map<Product>(product);
            var result = await _productRepository.AddProduct(model);
            return new ResponseBase<AddUpdateProductViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<AddUpdateProductViewModel>(result.Entity),
                Message = result.Message
            };
        }

        public async Task<ResponseBase<AddUpdateProductViewModel>> UpdateProduct(AddUpdateProductViewModel product)
        {
            var model = _mapper.Map<Product>(product);
            var result = await _productRepository.UpdateProduct(model);
            return new ResponseBase<AddUpdateProductViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<AddUpdateProductViewModel>(result.Entity),
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ProductViewModel>> DeleteProduct(int id)
        {

            var result = await _productRepository.Delete(id);
            return new ResponseBase<ProductViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<ProductViewModel>(result.Entity),
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ProductViewModel>> GetProduct(int id)
        {
            var result = await _productRepository.GetProduct(id);
            return new ResponseBase<ProductViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<ProductViewModel>(result.Entity),
                Message = result.Message
            };
        }


        public async Task<ResponseBase<IEnumerable<ProductViewModel>>> GetProductsList()
        {
            var result = await _productRepository.ProductsList();
            return new ResponseBase<IEnumerable<ProductViewModel>>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<IEnumerable<ProductViewModel>>(result.Entity),
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ProductViewModel>>> GetProductsListByType(string type)
        {
            var result = await _productRepository.ProductsList();
            var model = result.Entity.Where(i => i.Type == type);
            return new ResponseBase<IEnumerable<ProductViewModel>>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<IEnumerable<ProductViewModel>>(model),
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ProductViewModel>>> GetArtistProduct(string userId)
        {
            var result = await _productRepository.GetArtistProduct(userId);
            return new ResponseBase<IEnumerable<ProductViewModel>>()
            {
                IsSuccessful = result.IsSuccessful,
                Entity = _mapper.Map<IEnumerable<ProductViewModel>>(result.Entity),
                Message = result.Message
            };
        }

        //public async Task<ResponseBase<IEnumerable<ProductViewModel>>> GetByProductByType(string type)
        //{
        //    var result = await _productRepository.GetProductByType(type);
        //    return new ResponseBase<IEnumerable<ProductViewModel>>()
        //    {
        //        IsSuccessful = result.IsSuccessful,
        //        Entity = _mapper.Map<IEnumerable<ProductViewModel>>(result.Entity),
        //        Message = result.Message
        //    };
        //}
    }
}
