namespace trip_organizer_api.src.Api.Features.Auth.DTOs
{
    public class EmailPasswordLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
