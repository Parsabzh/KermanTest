using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KermanCraft.Domain.Models.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Status { get; set; }
        public int LanguageId { get; set; }

        public string PeopleId { get; set; }
        public People.People People { get; set; }
    }
}
