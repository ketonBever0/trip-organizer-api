namespace trip_organizer_api.src.Api.Features.Auth.DTOs
{
    public class SignUpDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Mobile { get; set; }
        public string? Nick { get; set; }

    }
}
