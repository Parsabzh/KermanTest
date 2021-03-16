using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Admin
{
   public class AdminListViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Family")]
        public string Family { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Mobile")]
        [MaxLength(11, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Status")]
        public bool Status { get; set; }

    }
}
