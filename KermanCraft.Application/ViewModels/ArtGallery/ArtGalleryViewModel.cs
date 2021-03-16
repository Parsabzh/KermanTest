using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Course;
using KermanCraft.Application.ViewModels.Image;

namespace KermanCraft.Application.ViewModels.ArtGallery
{
   public class ArtGalleryViewModel
    {
        public ArtGalleryViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(5);
            CommentViewModels = new List<CommentViewModel>();
        }

        [Key]
        public int ArtGalleryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }

        public List<ImageViewModel> ImageViewModels { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }
    }
}
