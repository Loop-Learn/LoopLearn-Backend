using LoopLearn.DataAccess.Services.Auth;
using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Models;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IAuthService authService;
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }
        [HttpPost("register")]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = authService.Register(registerDTO);
            if (!result.IsAuthenticated) 
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("login")]
        [Consumes("application/json")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = authService.Login(loginDTO);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    
    }
}
