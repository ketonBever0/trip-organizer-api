using Microsoft.AspNetCore.Mvc;
using System.Net;
using trip_organizer_api.src.Api.Features.Auth.DTOs;
using trip_organizer_api.src.Application;
using trip_organizer_api.src.Application.Exceptions;

namespace trip_organizer_api.src.Api.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IDBContext _ctx;
        public AuthController(IDBContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login/emailpassword")]
        public async Task<IActionResult> Login([FromBody] EmailPasswordLoginDto dto)
        {
            try
            {
                var token = await _ctx.Auth.LoginWithEmailAndPasswordAsync(dto.Email, dto.Password);
                return Ok(new { Token = token });
            }
            catch (BadCredentialsException)
            {
                return Unauthorized("Invalid credentials.");
            }
        }
    }
}
