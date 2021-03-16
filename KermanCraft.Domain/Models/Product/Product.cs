using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KermanCraft.Infrastructure.Localize.ProductResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Domain.Models.Product
{
   public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string ProductName { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "Description")]
        [MaxLength(300,ErrorMessageResourceType = typeof(ValidationResource),ErrorMessageResourceName = "MaxLength")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "status")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public bool Status { get; set; }
        public int LanguageId { get; set; }



        [Display(ResourceType = typeof(ProductResource),Name = "HasDiscount")]
        public bool HasDiscount { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "DiscountPrice")]
        [Column(TypeName = "money")]
        public decimal DiscountPrice { get; set; }

        //[Required]
        //public string ImageName { get; set; }

        public string Weight { get; set; }
        public string Type { get; set; }
        public bool IsForSale { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Code { get; set; }
        
        public string PeopleId { get; set; }
        public People.People People { get; set; }
    }
}
