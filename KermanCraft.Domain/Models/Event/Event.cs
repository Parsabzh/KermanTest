using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using KermanCraft.Infrastructure.Localize.EventResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Domain.Models.Event
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Date")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public DateTime Date { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Time")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Time { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(EventResource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public bool Status { get; set; }

        public string Type { get; set; }

        [Column(TypeName = ("money"))]
        public decimal Price { get; set; }
        public bool IsSale { get; set; }
        public int LanguageId { get; set; }

        public string ImageUrl { get; set; }
        public string PeopleFkId { get; set; }
    
    }
}
