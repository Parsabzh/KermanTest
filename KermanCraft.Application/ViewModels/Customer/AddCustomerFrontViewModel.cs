using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Customer
{
   public class AddCustomerFrontViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Family")]
        public string Family { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "PasswordConfirm")]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Email")]
        //[Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Mobile")]
        [MaxLength(11, ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "MaxLength")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Birthday")]
        public string Birthday { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "Status")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "UserName")]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        //[Display(ResourceType = typeof(PeopleResource), Name = "Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(PeopleResource), Name = "NationalId")]
        [StringLength(10, ErrorMessageResourceType = typeof(ValidationResource),
             ErrorMessageResourceName = "StringLength"), MinLength(10)]
        public string NationalId { get; set; }

        public string ImageUrl { get; set; }


        public int LanguageId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "Code")]
        public int Code { get; set; }
    }
}
