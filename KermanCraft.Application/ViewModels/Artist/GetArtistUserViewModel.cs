using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Artist
{
    public class GetArtistUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationResource),ErrorMessageResourceName="Required")]
        [Display(ResourceType = typeof(PeopleResource),Name = "Mobile")]
        public string Mobile { get; set; }
    }
}
