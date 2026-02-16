namespace SocialMedia.Models
{
    public class CommentRequest
    {
        public int PostId { get; set; }
        public string Content { get; set; } = "";
    }
}
