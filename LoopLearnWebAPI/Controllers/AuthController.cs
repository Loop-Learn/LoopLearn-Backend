using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static LoopLearn.Entities.utils.Enums;

namespace LoopLearnWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _config;
        public AuthController(IUnitOfWork _unitOfWork, IConfiguration config)
        {
            unitOfWork = _unitOfWork;
            _config = config;
        }
        [HttpPost("register")]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            var userNameExists = unitOfWork.User.Exists(u => u.Username == registerDTO.Username);
            if (userNameExists) return BadRequest("Username is already Exist.");

            var EmailExists = unitOfWork.User.Exists(u => u.Email == registerDTO.Email);
            if (EmailExists) return BadRequest("Email is already Exist.");

            var newStudent = new Student()
            {
                FName = registerDTO.FName,
                LName = registerDTO.LName,
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                BirthDate = registerDTO.BirthDate,
                Phone = registerDTO.Phone,
                Gender = registerDTO.Gender,
                Role = RoleType.Student,
                Avatar = "not attached"

            };
            unitOfWork.User.Add(newStudent);
            unitOfWork.Save();
            return Created();
        }
        [HttpPost("login")]
        [Consumes("application/json")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = unitOfWork.User.GetFirstOrDefault(u=>u.Username == loginDTO.Username || u.Email==loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                return Unauthorized("Invalid credentials. Incorrect Username/Email or Password ");
            }

            var token = GenerateToken(user);
            return Ok(new { token = token });
        }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()) 
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
