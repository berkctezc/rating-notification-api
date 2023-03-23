namespace Domain.Settings.RatingApi;

[ExcludeFromCodeCoverage]
public class ServiceSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string QConnectionString { get; set; } = string.Empty;
}