using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using SocialMedia.Models;
using System.Linq;

namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(UserModel user)
        {
            

            var dbUser = _context.Users
                .FirstOrDefault(u => u.Username == user.Username);

            if (dbUser == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(user);
            }

            if (dbUser.Password != user.Password)
            {
                ModelState.AddModelError("", "Invalid password");
                return View(user);
            }

            return RedirectToAction("HomeDashboard", new
            {
                Id=dbUser.Id,
            });
        }


        public IActionResult HomeDashboard(int id)
        {

            List<PostModel> posts = _context.Posts.ToList();

            ViewBag.UserId=id;
            return View(posts);
        }

        public IActionResult UserDashboard(int UserId)
        {
            var user = _context.Users
                .Include(u => u.Posts)
                .FirstOrDefault(u=> u.Id == UserId);

            return View(user);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel User)
        {
            if (!ModelState.IsValid)
            return View(User);

            _context.Users.Add(User);
            _context.SaveChanges();

            return RedirectToAction("HomeDashboard", new
            {
                Id=User.Id,
            });

        }
    }
}
