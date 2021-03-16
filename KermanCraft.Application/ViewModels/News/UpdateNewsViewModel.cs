using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Application.ViewModels.Image;

namespace KermanCraft.Application.ViewModels.News
{
   public class UpdateNewsViewModel
    {
        public UpdateNewsViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(5);
        }
        public int NewsId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }

        public int LanguageId { get; set; }

        public IList<ImageViewModel> ImageViewModels { get; set; }
    }
}
