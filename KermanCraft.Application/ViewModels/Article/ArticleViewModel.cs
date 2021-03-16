using KermanCraft.Application.ViewModels.Image;
using System.Collections.Generic;
using KermanCraft.Application.ViewModels.Comment;

namespace KermanCraft.Application.ViewModels.Article
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(5);
            CommentViewModels = new List<CommentViewModel>();
        }
        public int ArticleId { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public bool Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string PdfUrl { get; set; }
        public int LanguageId { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }
        public string PeopleId { get; set; }
        public List<ImageViewModel> ImageViewModels { get; set; }

    }
}
