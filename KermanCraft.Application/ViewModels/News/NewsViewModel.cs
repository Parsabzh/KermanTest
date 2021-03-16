using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Image;

namespace KermanCraft.Application.ViewModels.News
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(5);
            CommentViewModels = new List<CommentViewModel>();
        }
        public int NewsId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }

        public string Author { get; set; }
        public List<ImageViewModel> ImageViewModels { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }
    }
}
