using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KermanCraft.Application.ViewModels.Comment;
using KermanCraft.Infrastructure.Localize.EventResource;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Event
{
   public class EventViewModel
    {
        public EventViewModel()
        {
            CommentViewModels = new List<CommentViewModel>();
        }
        public int EventId { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Date")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Date { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Time")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Time { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public bool Status { get; set; }

        //[Display(ResourceType = typeof(PeopleResource), Name = "Mobile")]
        //[Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        //public string PhoneNumber { get; set; }
        //public string Type { get; set; }
        public bool IsSale { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string PeopleId { get; set; }
        public int LanguageId { get; set; }
        public IList<CommentViewModel> CommentViewModels { get; set; }


    }
}
