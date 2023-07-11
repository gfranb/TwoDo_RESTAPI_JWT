using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwoDo.Data;
using TwoDo.Models;

namespace TwoDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly TwoDoDbContext _db;
        private readonly IConfiguration _configuration;
        public AuthenticationController(TwoDoDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO request)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if( user == null)
            {
                return BadRequest("Wrong email");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            var userToken = GenerateJWT(user);
            return Ok(userToken);
        }

        private string GenerateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: cred,
                    expires: DateTime.Now.AddDays(1)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
