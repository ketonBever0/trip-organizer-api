using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using trip_organizer_api.src.Api.Features.Auth.DTOs;
using trip_organizer_api.src.Application;
using trip_organizer_api.src.Application.Exceptions;

namespace trip_organizer_api.src.Api.Features.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IDBContext _ctx;
        public AuthController(IDBContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyToken([FromHeader(Name = "Authorization")] string? authorization)
        {
            Console.WriteLine(Request.Headers.Authorization);
            if (string.IsNullOrWhiteSpace(authorization))
            {
                return Unauthorized("No token.");
            }

            try
            {
                var result = await _ctx.Auth.VerifyIdTokenAsync(authorization.Split(" ")[1]);
                return Ok(new { IsValid = true, Id = result.Uid });
            }
            catch (Exception)
            {
                return Unauthorized("Invalid token.");
            }
        }

        [AllowAnonymous]
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
