using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Infrastructure.Localize.ProductResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Product
{
  public class ProductViewModel
    {
        public ProductViewModel()
        {
            CommentViewModels = new List<CommentViewModel>();
            ImageViewModels = new List<ImageViewModel>();
            ProductListViewModels = new List<ProductListViewModel>();

        }
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
        [MaxLength(300, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "status")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "HasDiscount")]
        public bool HasDiscount { get; set; }

        [Display(ResourceType = typeof(ProductResource), Name = "DiscountPrice")]
        [Column(TypeName = "money")]
        public decimal DiscountPrice { get; set; }
        public string ImageName { get; set; }

        public string Weight { get; set; }
        public int LanguageId { get; set; }

        public string Type { get; set; }
        public bool IsForSale { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public string Code { get; set; }
        public string PeopleId { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }

        public List<ImageViewModel> ImageViewModels { get; set; }
        public List<ProductListViewModel> ProductListViewModels { get; set; }

        public string OwnerMobile { get; set; }
    }
}
