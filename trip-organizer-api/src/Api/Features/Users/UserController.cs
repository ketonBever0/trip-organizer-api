using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trip_organizer_api.src.Application;
using trip_organizer_api.src.Application.Interfaces;

namespace trip_organizer_api.src.Api.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDBContext _ctx;

        public UserController(IDBContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            Request.Headers.TryGetValue("AuthOnly", out var authOnly);
            var result = authOnly == "true" ? await _ctx.User.ListAuthUsersAsync() : await _ctx.User.ListUsersAsync();

            return Ok(result);
        }
    }
}
