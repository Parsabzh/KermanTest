using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Company
{
   public class CompanyMember
    {
        [Key]
        public int CompanyMemberId { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string PeopleFkId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
