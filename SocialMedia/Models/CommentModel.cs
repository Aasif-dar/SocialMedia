using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;

        public int PostId { get; set; }
        public PostModel Post { get; set; } = null!;
    }
}
