using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KermanCraft.Infrastructure.Localize.ProductResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Product
{
   public class ProductListViewModel
    {
        public int ProductId { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string ProductName { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Code { get; set; }
        public string ImageName { get; set; }
        public string PeopleId { get; set; }


    }
}
