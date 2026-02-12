using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context) => _context = context;

        public IActionResult Login() => View();


        [HttpPost]
        public IActionResult Login([FromBody] UserModel model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username
                                  && u.Password == model.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("JWT_SECRET_KEY_123")
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }


        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register([FromBody]UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new { message = "User created" });
        }
    }
}
