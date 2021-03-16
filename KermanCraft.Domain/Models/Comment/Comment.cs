using System;
using System.ComponentModel.DataAnnotations;
using KermanCraft.Infrastructure.Localize.CommentResource;
using KermanCraft.Infrastructure.Localize.ValidationResource;

namespace KermanCraft.Domain.Models.Comment
{
   public class Comment
    {
        public int CommentId { get; set; }

        [Display(ResourceType = typeof(CommentResource),Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(ValidationResource),ErrorMessageResourceName = "Required")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(CommentResource), Name = "LikeCount")]
        public int LikeCount { get; set; } = 0;

        [Display(ResourceType = typeof(CommentResource), Name = "DislikeCount")]
        public int DisLike { get; set; } = 0;
        public string PeopleId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public People.People People { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
       

    }
}
