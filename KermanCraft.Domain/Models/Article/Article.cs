using System;
using System.ComponentModel.DataAnnotations;

namespace KermanCraft.Domain.Models.Article
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public string PdfUrl { get; set; }
        public int LanguageId { get; set; }

        public string PeopleId { get; set; }
        public bool Status { get; set; }
        public People.People People { get; set; }
    }
}
