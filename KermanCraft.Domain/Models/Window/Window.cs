using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Window
{
   public  class Window
    {
        [Key]
        public int WindowId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public string VideoUrl { get; set; }


    }
}
