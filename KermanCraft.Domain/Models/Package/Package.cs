using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KermanCraft.Domain.Models.Package
{
   public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Period { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int ProductNum { get; set; }
        public int Score { get; set; }
        public int LanguageId { get; set; }


    }
}
