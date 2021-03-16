using System;
using System.Collections.Generic;
using System.Text;
using KermanCraft.Domain.Models.People;

namespace KermanCraft.Application.ViewModels.Package
{
    public class PeoplePackageViewModel
    {
        public int PeoplePackageId { get; set; }
        public int PackageId { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PeopleId { get; set; }
        public bool IsPaid { get; set; }
        public string ReferenceId { get; set; }

    }
}
