namespace SocialMedia.Models
{
    public class LikeModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public PostModel Post { get; set; } = null!;

        public int UserId { get; set; }
        public UserModel User { get; set; } = null!;
    }
}
