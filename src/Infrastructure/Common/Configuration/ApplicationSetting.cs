namespace Infrastructure.Common.Configuration
{
    public class ApplicationSetting
    {
        public DatabaseConnection? DatabaseConnection { get; set; }
    }

    public class DatabaseConnection
    {
        public string? DbConnection { get; set; }
    }
}
