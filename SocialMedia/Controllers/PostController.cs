using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int UserId)
        {
            var post = new PostModel
            {
                UserId = UserId,
            };
                
            return View(post);
        }

        [HttpPost]
        public IActionResult Create(PostModel post)
        {

            _context.Add(post);
            _context.SaveChanges();


             return RedirectToAction("UserDashboard", "User", new
            {
                UserId = post.UserId
            });

        }

        public IActionResult Delete(int UserId)
        {
            if(UserId != null)
            {
                _context.Posts.Remove(_context.Posts.Find(UserId)!);
                _context.SaveChanges();

                return RedirectToAction("HomeDashboard", "User");
            }

            return View();
        }
    }
}
