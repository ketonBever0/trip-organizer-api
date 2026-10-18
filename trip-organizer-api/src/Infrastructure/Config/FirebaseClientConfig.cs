namespace trip_organizer_api.src.Infrastructure.Config
{
    public class FirebaseClientConfig
    {
        public required string ApiKey { get; set; }
        public required string AuthDomain { get; set; }
        public required string ProjectId { get; set; }
        public required string StorageBucket { get; set; }
        public required string MessagingSenderId { get; set; }
        public required string AppId { get; set; }
        public required string MeasurementId { get; set; }
    }
}
