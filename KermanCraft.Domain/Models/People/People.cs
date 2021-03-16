using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KermanCraft.Domain.Models.Package;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;
using Microsoft.AspNetCore.Identity;

namespace KermanCraft.Domain.Models.People
{
    public class People : IdentityUser
    {
        [Display(ResourceType = typeof(PeopleResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Family")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [MaxLength(50, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string Family { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Birthday { get; set; }
        public bool Status { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "NationalId")]
        [StringLength(10, ErrorMessageResourceType = typeof(ValidationResource),
            ErrorMessageResourceName = "StringLength"), MinLength(10)]
        public string NationalId { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "حیطه کاری")]
        //  [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Job { get; set; }

        [Display(Name = "برند")]
        //  [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Brand { get; set; }
        public int LanguageId { get; set; }

        public string Education { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Telegram { get; set; }
        public string WhatsApp { get; set; }

        public string Achievement { get; set; }

        public ICollection<Product.Product> Products { get; set; }
        public ICollection<Comment.Comment> Comments { get; set; }
        public ICollection<Article.Article> Articles { get; set; }


    }
}
