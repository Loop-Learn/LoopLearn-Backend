using LoopLearn.DataAccess.Services.Auth;
using LoopLearn.Entities.DTO;
using LoopLearn.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = authService.Register(registerDTO);

            if (!result.IsAuthenticated)
                return BadRequest(new { message = result.Message }); // ✅ object not string

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
                return BadRequest(new { message = result.Message }); // ✅ object not string

            return Ok(result);
        }
    }
}