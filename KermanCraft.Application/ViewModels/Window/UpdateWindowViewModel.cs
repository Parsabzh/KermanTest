using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.Image;

namespace KermanCraft.Application.ViewModels.Window
{
    public class UpdateWindowViewModel
    {
        public UpdateWindowViewModel()
        {
             ImageViewModels = new List<ImageViewModel>();
        }
        public int WindowId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public string VideoUrl { get; set; }
        public List<ImageViewModel> ImageViewModels { get; set; }
        public string Category { get; set; }

    }
}
