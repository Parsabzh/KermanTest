using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Image
{
   public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ProductFkId { get; set; }
        public int WindowFkId { get; set; }
        public int NewsFkId { get; set; }
        public int GalleryFkId { get; set; }
        public int ArtGalleryFkId { get; set; }
        public int ArticleFkId { get; set; }
    }
}
