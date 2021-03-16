using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IProductTypeService
    {
        Task<ResponseBase<ProductTypeViewModel>> AddProductType(ProductTypeViewModel productType);
        Task<ResponseBase<ProductTypeViewModel>> UpdateProductType(ProductTypeViewModel productType);
        Task<ResponseBase<ProductTypeViewModel>> DeleteProductType(int productTypeId);
        Task<ResponseBase<ProductTypeViewModel>> GetProductType(int productTypeId);
        Task<ResponseBase<IEnumerable<ProductTypeViewModel>>> GetAllProductType();
    }
}
