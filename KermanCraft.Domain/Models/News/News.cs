using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.News
{
   public class News
    {
        [Key]
        public int NewsId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int LanguageId { get; set; }

    }
}
