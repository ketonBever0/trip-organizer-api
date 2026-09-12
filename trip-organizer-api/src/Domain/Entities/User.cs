using FirebaseAdmin.Auth;

namespace trip_organizer_api.src.Domain.Entities
{
    public class User
    {
        public required string ID { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public string? Nick { get; set; }
        public required UserRecord AuthRecord { get; set; }
    }
}
