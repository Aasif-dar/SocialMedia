using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = "";

        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        public ICollection<CommentModel> Comments { get; set; }
            = new List<CommentModel>();

        public ICollection<LikeModel> Likes { get; set; }
            = new List<LikeModel>();
    }
}
