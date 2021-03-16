using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Application.ViewModels.Image
{
   public class ImageListViewModel
    {
        public ImageListViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(50);
            
        }
        public List<ImageViewModel> ImageViewModels { get; set; }
        public int Id { get; set; }
    }
}
