using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Domain.Models.Product
{
   public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public string ImageName { get; set; }
    }
}
