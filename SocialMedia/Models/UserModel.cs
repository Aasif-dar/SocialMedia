namespace SocialMedia.Models;
public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public ICollection<PostModel> Posts { get; set; } = new List<PostModel>();
    public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
    public ICollection<LikeModel> Likes { get; set; } = new List<LikeModel>();
}
