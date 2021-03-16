using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Language
{
    public class Lang
    {
        [Key]
        public int LangId { get; set; }
        public string Title { get; set; }
    }
}
