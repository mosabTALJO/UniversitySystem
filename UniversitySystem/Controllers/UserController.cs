using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        public UserController(DataContext context, IMapper mapper, IConfiguration config, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("SignUp")]
        public async Task<ActionResult<bool>> SignUp(UserVM userVM)
        {
            //userVM.Id = string.Empty;
            //var newUser = new User
            //{
            //    Email = userVM.Email,
            //    PasswordHash = userVM.Password,
            //    UserName = userVM.UserName,
            //};

            //await _context.Users.AddAsync(newUser);
            //await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.DisplayName == username && u.PasswordHash == password);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user is null)
                return BadRequest("Username or password is incorrect");

            if (!await _userManager.CheckPasswordAsync(user, password))
                return BadRequest("Password is incorrect");

            var token = GenerateToken(user);
            return Ok(token);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Jwt:Site").Value,
                audience: _config.GetSection("Jwt:Site").Value,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
