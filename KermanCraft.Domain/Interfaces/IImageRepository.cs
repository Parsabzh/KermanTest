using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Image;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
   public interface IImageRepository
   {
       Task<ResponseBase<Image>> AddImage(Image img);
       Task<ResponseBase<IEnumerable<Image>>> GetWindowsImage(int winId);
       Task<ResponseBase<IEnumerable<Image>>> GetNewsImage(int newsId);
       Task<ResponseBase<IEnumerable<Image>>> GetGalleryImage(int galleryId);
       Task<ResponseBase<IEnumerable<Image>>> GetArtGalleryImage(int artGallery);
       Task<ResponseBase<IEnumerable<Image>>> GetProductImage(int productId);
       Task<ResponseBase<IEnumerable<Image>>> GetArticleImage(int articleId);
       Task<ResponseBase<Image>> DeleteImage(int imgId);
       Task<ResponseBase<Image>> DeleteProductImage(int productId);
       Task<ResponseBase<Image>> DeleteArticleImage(int articleId);
        Task<ResponseBase<Image>> DeleteProductImage(int productId,string imageUrl);
       Task<ResponseBase<Image>> DeleteWindowsImage(int windowId);
       Task<ResponseBase<Image>> DeleteWindowsImage(int windowId, string imageUrl);
       Task<ResponseBase<Image>> DeleteNewsImage(int newsId);
       Task<ResponseBase<Image>> DeleteNewsImage(int newsId, string imageUrl);
       Task<ResponseBase<Image>> DeleteGalleryImage(int galleryId);
       Task<ResponseBase<Image>> DeleteGalleryImage(int galleryId,string imageUrl);
       Task<ResponseBase<Image>> DeleteArtGalleryImage(int artGalleryId);
       Task<ResponseBase<Image>> DeleteArtGalleryImage(int artGalleryId, string imageUrl);


   }
}
