using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.Tools.PersianDate;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class ProductTypeService:IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeService(IMapper mapper, IProductTypeRepository productTypeRepository)
        {
            _mapper = mapper;
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ResponseBase<ProductTypeViewModel>> AddProductType(ProductTypeViewModel productType)
        {
            var type = _mapper.Map<ProductType>(productType);
            var result = await _productTypeRepository.AddProductType(type);
            return new ResponseBase<ProductTypeViewModel>()
            {
                Entity = _mapper.Map<ProductTypeViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<ProductTypeViewModel>> UpdateProductType(ProductTypeViewModel productType)
        {
          
            var result = await _productTypeRepository.UpdateProductType(_mapper.Map<ProductType>(productType));
            return new ResponseBase<ProductTypeViewModel>()
            {
                Entity = _mapper.Map<ProductTypeViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ProductTypeViewModel>> DeleteProductType(int productTypeId)
        {
            var result = await _productTypeRepository.DeleteProductType(productTypeId);
            return new ResponseBase<ProductTypeViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<ProductTypeViewModel>> GetProductType(int productTypeId)
        {
            var result = await _productTypeRepository.GetProductType(productTypeId);
            return new ResponseBase<ProductTypeViewModel>()
            {
                Entity = _mapper.Map<ProductTypeViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<ProductTypeViewModel>>> GetAllProductType()
        {

            var result = await _productTypeRepository.GetAllProductType();
            return new ResponseBase<IEnumerable<ProductTypeViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<ProductTypeViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
