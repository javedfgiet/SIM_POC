using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sim_backend_api.Data;
using sim_backend_api.DTOs.Auth;
using sim_backend_api.Models;
using sim_backend_api.Services.Token;
using System.Threading.Tasks;

namespace sim_backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AppDbContext context, 
        ITokenService tokenService,
        IPasswordHasher<User> _passwordHasher) : ControllerBase
    {
        private static User? user = new();
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            if (new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("InCorrect Password");
            }


            var token = tokenService.GenerateToken(user);
            return Ok(new
            {
                AccessToken = token
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(RegisterDto dto)
        {
            var isAlreadyRegitered = await context.Users
                .AnyAsync(x => x.UserName == dto.Username);
            if (isAlreadyRegitered)
                return BadRequest("User already exists");
            var user = new User
            {
                UserName = dto.Username,

            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok("Registartion Successfull");
        }

    }
}
