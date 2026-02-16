using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using System.Security.Claims;


namespace SocialMedia.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly ApplicationDbContext _context; 


        public UserDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var user = _context.Users
                .Include(u => u.Posts)
                .FirstOrDefault(u => u.Id == userId);

            return View(user);
        }




    }
}
