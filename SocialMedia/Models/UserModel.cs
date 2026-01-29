using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{

    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>();

        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }

}

