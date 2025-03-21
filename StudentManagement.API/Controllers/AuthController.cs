using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Models;

namespace StudentManagement.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtService _jwtService;
        private IUserService _userService;
        public AuthController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.Login(new LoginUserModel
            {
                EmailOrUsername = request.Email,
                Password = request.Password
            });
            if (user is null)
                return new UnauthorizedObjectResult("Wrong email, username or password");
            var token = _jwtService.GenerateToken(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Username", user.Username)
            });

            return new OkObjectResult(token);
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserModel request)
        {
            var user = await _userService.Register(request);
            if (user is null)
                return new BadRequestObjectResult("Username already existed");
            var token = _jwtService.GenerateToken(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Username", user.Username)
            });

            return new CreatedResult("", token);
        }
    }
}
