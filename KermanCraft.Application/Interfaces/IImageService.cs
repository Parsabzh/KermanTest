using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Domain.Models.Image;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
   public interface IImageService
    {
        Task<ResponseBase<ImageViewModel>> AddImage(ImageViewModel img);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetWindowsImage(int winId);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetNewsImage(int newsId);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetGalleryImage(int galleryId);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetArtGalleryImage(int artGallery);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetProductImage(int productId);
        Task<ResponseBase<ImageViewModel>> DeleteImage(int imgId);
        Task<ResponseBase<ImageViewModel>> DeleteProductImage(int productId);
        Task<ResponseBase<ImageViewModel>> DeleteWindowsImage(int windowId);
        Task<ResponseBase<ImageViewModel>> DeleteNewsImage(int newsId);
        Task<ResponseBase<ImageViewModel>> DeleteGalleryImage(int galleryId);
        Task<ResponseBase<ImageViewModel>> DeleteArtGalleryImage(int artGalleryId);
        Task<ResponseBase<IEnumerable<ImageViewModel>>> GetArticleImage(int articleId);
        Task<ResponseBase<ImageViewModel>> DeleteArticleImage(int articleId);


    }
}
