using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            // 1. Шукаємо користувача в БД (асинхронно)
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserLogin == request.Login);
            if (user == null)
                return Unauthorized(); // Повертаємо 401 без уточнення, що саме невірно

            // 2. Перевіряємо хеш пароля
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.UserPassword, request.Password);

            if (result != PasswordVerificationResult.Success)
                return Unauthorized();

            // 3. Генеруємо JWT
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: new[] { new Claim(ClaimTypes.NameIdentifier, user.UserLogin) },
                expires: DateTime.UtcNow.AddMinutes(15), // Короткий час життя токена для безпеки
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
