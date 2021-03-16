using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Application.ViewModels.Image;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Gallery
{
   public class GalleryViewModel
    {
        public GalleryViewModel()
        {
            ImageViewModels = new List<ImageViewModel>(20);
        }
        public int GalleryId { get; set; }

        [Display(Name = "نام گالری")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource),ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Description { get; set; }
        public bool Status { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string EndDate { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string PhoneNumber { get; set; }

        public string PeopleId { get; set; }
        public int LanguageId { get; set; }

        public List<ImageViewModel> ImageViewModels { get; set; }
    }
}
