using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace KermanCraft.Domain.Models.Package
{
    public class PeoplePackage
    {
        [Key]
        public int PeoplePackageId { get; set; }
        public int PackageId { get; set; }
        public string PeopleId { get; set; }
        public bool IsPaid { get; set; }
        public string ReferenceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public Package Package { get; set; }
        public People.People People { get; set; }

    }
}
