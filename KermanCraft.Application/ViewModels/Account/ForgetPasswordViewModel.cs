using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KermanCraft.Infrastructure.Localize.PeopleResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(PeopleResource), Name = "Password")]
        public string NewPass { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(PeopleResource), Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPass), ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "PasswordConfirm")]
        public string NewPassConfirm { get; set; }

        public string UserId { get; set; }
    }
}
