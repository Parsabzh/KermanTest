using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Application.ViewModels.Company
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            CompanyMemberViewModels = new List<CompanyMemberViewModel>(20);
        }
        public int CompanyId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string RegistrationNum { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        [Display(Name = "نوع")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Type { get; set; }
        public bool Status { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "Required")]
        public string Tel { get; set; }
        public string OwnerPhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Telegram { get; set; }
        public string WhatsApp { get; set; }
        public string Job { get; set; }
        public string PeopleId { get; set; }
        public List<CompanyMemberViewModel> CompanyMemberViewModels { get; set; }
        public int LanguageId { get; set; }

    }
}
