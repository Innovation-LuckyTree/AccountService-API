namespace AccountService.Infrastructure.Common.Config;

/// <summary>
/// Record model containing all the configuration settings for the Application.
/// </summary>
public record AppSettings
{
    /// <summary>
    /// List of API settings.
    /// </summary>
    public IEnumerable<ApiSettings> ApiSettings { get; init; } = null!;

}
