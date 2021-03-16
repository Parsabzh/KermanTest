using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Tag
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Subject { get; set; }
        public int ProductFkId { get; set; }
        public int NewsFkId { get; set; }
        public int ArticleFkId { get; set; }

    }
}
