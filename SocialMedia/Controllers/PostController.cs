using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==============================
        // SHOW ALL POSTS
        // ==============================
        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.Id)
                .ToList();

            return View(posts);
        }

        // ==============================
        // CREATE POST
        // ==============================
        [HttpPost]
        public IActionResult Create(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return RedirectToAction("Index");

            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var post = new PostModel
            {
                Content = content,
                UserId = userId
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ==============================
        // LIKE POST (AJAX)
        // ==============================
        [HttpPost]
        public IActionResult Like(int postId)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var alreadyLiked = _context.Likes
                .Any(l => l.PostId == postId && l.UserId == userId);

            if (!alreadyLiked)
            {
                _context.Likes.Add(new LikeModel
                {
                    PostId = postId,
                    UserId = userId
                });

                _context.SaveChanges();
            }

            var count = _context.Likes.Count(l => l.PostId == postId);

            return Json(new { count });
        }

        // ==============================
        // ADD COMMENT (AJAX)
        // ==============================
        [HttpPost]
        public IActionResult AddComment(int postId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest();

            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var comment = new CommentModel
            {
                PostId = postId,
                UserId = userId,
                Content = content
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Json(new
            {
                id = comment.Id,
                content = comment.Content
            });
        }
    }
}
