using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Artist
{
    public class ArtistListViewModel
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
        public int LanguageId { get; set; }

        public string Brand { get; set; }
        public string Job { get; set; }
        public string ImageUrl { get; set; }
        public string Education { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Telegram { get; set; }
        public string WhatsApp { get; set; }
    }
}
