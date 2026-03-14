using Microsoft.AspNetCore.Mvc;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.DTO;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("user/register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = _authService.Register(request);
            return Ok(result);
        }

        [HttpPost("user/login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _authService.Login(request);
            return Ok(new { Token = result });
        }
            

        [HttpPost("admin/login")]
        public IActionResult AdminLogin([FromBody] AdminLoginRequest request)
        {
            var result = _authService.AdminLogin(request);
            return Ok(new { Token = result });
        }
    }
}
