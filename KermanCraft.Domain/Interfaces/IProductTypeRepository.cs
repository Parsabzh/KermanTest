using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<ResponseBase<ProductType>> AddProductType(ProductType productType);
        Task<ResponseBase<ProductType>> UpdateProductType(ProductType productType);
        Task<ResponseBase<ProductType>> DeleteProductType(int productTypeId);
        Task<ResponseBase<ProductType>> GetProductType(int productTypeId);
        Task<ResponseBase<IEnumerable<ProductType>>> GetAllProductType();
    }
}
