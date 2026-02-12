using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show all posts
        public IActionResult Index()
        {
            var posts = _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ToList();

            return View(posts);
        }

        // Create new post
        [Authorize]
        [HttpPost]
        public IActionResult Create(string content)
        {
            var userId = int.Parse(
                HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!
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

        // Like post
        [Authorize]
        [HttpPost]
        public IActionResult Like(int postId)
        {
            var userId = int.Parse(
                HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            if (!_context.Likes.Any(l => l.PostId == postId && l.UserId == userId))
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

        // Add comment
        [Authorize]
        [HttpPost]
        public IActionResult AddComment(int postId, string content)
        {
            var userId = int.Parse(
                HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var comment = new CommentModel
            {
                PostId = postId,
                UserId = userId,
                Content = content
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Json(comment);
        }
    }
}
