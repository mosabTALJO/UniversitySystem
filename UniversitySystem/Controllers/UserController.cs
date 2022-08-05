using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserController(DataContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        //[Authorize(Roles = "Manager")]
        //[HttpPost("SignUp")]
        //public async Task<ActionResult<bool>> SignUp(User user)
        //{
        //    user.Id = string.Empty;
        //    var newUser = new User
        //    {
        //        Email = user.Email,
        //        PasswordHash = user.PasswordHash,
        //        DisplayName = user.DisplayName
        //    };

        //    await _context.Users.AddAsync(newUser);
        //    await _context.SaveChangesAsync();
        //    return Ok(true);
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.DisplayName == username && u.PasswordHash == password);
        //    if (user == null) return BadRequest("username or password are wrong");

        //    var token = GenerateToken(user);
        //    return Ok(token);
        //}

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                //new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
