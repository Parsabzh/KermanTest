using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Course
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            CommentViewModels = new List<CommentViewModel>();
        }
        public int CourseId { get; set; }
        [Display(Name = "موضوع")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Subject { get; set; }

        [Display( Name = "توضیحات")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Text { get; set; }

        public string DateTime { get; set; }
        public string ImageUrl { get; set; }
        [Display( Name = "قیمت")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public decimal Price { get; set; }
        //[Display(ResourceType = typeof(PeopleResource), Name = "Mobile")]
        //[Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        //public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string PeopleId { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }
        public int LanguageId { get; set; }

    }
}
