using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Gallery
{
   public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public int LanguageId { get; set; }

        public string PeopleId { get; set; }
        public People.People People { get; set; }
        public ICollection<Image.Image> Images { get; set; }

    }
}
