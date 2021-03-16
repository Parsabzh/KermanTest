using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Image;

namespace KermanCraft.Application.ViewModels.Window
{
    public class WindowViewModel
    {
        public WindowViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(5);
            CommentViewModels = new List<CommentViewModel>();
        }
        public int WindowId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public IList<ImageViewModel> ImageViewModels { get; set; }
        public string Category { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }

        public string VideoUrl { get; set; }

        public int LanguageId { get; set; }

    }
}
