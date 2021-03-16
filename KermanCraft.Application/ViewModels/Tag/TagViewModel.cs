using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Application.ViewModels.Tag
{
    public class TagViewModel
    {
        public int TagId { get; set; }
        public string Subject { get; set; }
        public int ProductFkId { get; set; }
        public int NewsFkId { get; set; }
        public int ArticleFkId { get; set; }
    }
}
