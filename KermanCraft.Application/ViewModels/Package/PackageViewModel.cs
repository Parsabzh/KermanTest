using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Package
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "نام پکیج")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "مدت")]
        public int Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "تعداد محصول")]
        public int ProductNum { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(Name = "امتیاز")]
        public int Score { get; set; }
    }
}
