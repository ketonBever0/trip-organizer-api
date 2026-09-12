namespace trip_organizer_api.src.Infrastructure.Context
{
    public class DBSelector
    {
        public required string DBType { get; set; }

        public DBSelector(IConfiguration config)
        {
            DBType = config.GetValue<string>("DBType") ?? throw new ArgumentNullException("DBType configuration is missing.");
        }
    }
}
