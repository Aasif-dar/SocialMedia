using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data;

namespace SocialMedia.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public  CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create(int Id)
        {


            return View();
        }
    }
}
