using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Domain.Models.Course
{
   public class CourseMember
    {
        public int CourseMemberId { get; set; }
        public int CourseId { get; set; }
        public string PeopleFkId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public string ReferenceId { get; set; }

        public Course Course { get; set; }
    }
}
