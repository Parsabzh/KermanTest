using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Product;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
   public interface IProductRepository
    {
        Task<ResponseBase<Product>> AddProduct(Product product);
        Task<ResponseBase<Product>> UpdateProduct(Product product);
        Task<ResponseBase<Product>> Delete(int id);
        Task<ResponseBase<Product>> GetProduct(int id);
        Task<ResponseBase<IEnumerable<Product>>> ProductsList();
        Task<ResponseBase<IEnumerable<Product>>> GetArtistProduct(string userId);
        Task<ResponseBase<IEnumerable<Product>>> GetProductByType(string type);

    }
}
