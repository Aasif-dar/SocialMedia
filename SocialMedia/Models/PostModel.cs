using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int UserId { get; set; }

        public UserModel User { get; set; } = null!;

        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}

