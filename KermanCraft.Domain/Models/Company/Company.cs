using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Company
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string RegistrationNum { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public string ImageUrl { get; set; }
        public string Tel { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Telegram { get; set; }
        public string WhatsApp { get; set; }
        public string Job { get; set; }
        public int LanguageId { get; set; }

        public string PeopleId { get; set; }
        public People.People People { get; set; }

        public ICollection<CompanyMember> CompanyMembers { get; set; }
    }
}
