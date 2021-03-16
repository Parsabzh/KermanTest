using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.ArtGallery
{
    public class ArtGallery
    {
        [Key]
        public int ArtGalleryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int LanguageId { get; set; }
    }
}
