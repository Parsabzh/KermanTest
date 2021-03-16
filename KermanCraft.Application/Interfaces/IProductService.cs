using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IProductService
    {
        Task<ResponseBase<AddUpdateProductViewModel>> AddProduct(AddUpdateProductViewModel product);
        Task<ResponseBase<AddUpdateProductViewModel>> UpdateProduct(AddUpdateProductViewModel product);
        Task<ResponseBase<ProductViewModel>> DeleteProduct(int id);
        Task<ResponseBase<ProductViewModel>> GetProduct(int id);
        Task<ResponseBase<IEnumerable<ProductViewModel>>> GetProductsList();
        Task<ResponseBase<IEnumerable<ProductViewModel>>> GetProductsListByType(string type);
        Task<ResponseBase<IEnumerable<ProductViewModel>>> GetArtistProduct(string userId);
    }
}
