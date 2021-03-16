using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Application.ViewModels.Company
{
    public class CompanyMemberViewModel
    {
        public int CompanyMemberId { get; set; }

        public string Name { get; set; }
        public string Family { get; set; }
        public string PeopleFkId { get; set; }
        public int CompanyId { get; set; }
        
    }
}
