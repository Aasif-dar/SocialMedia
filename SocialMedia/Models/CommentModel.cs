public class CommentModel
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public int PostId { get; set; }
    public int UserId { get; set; }
}
