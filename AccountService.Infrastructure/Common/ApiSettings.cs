namespace AccountService.Infrastructure.Common.Config;

/// <summary>
/// Record model that contains the settings for calling an API.
/// </summary>
public record ApiSettings
{
    /// <summary>
    /// The name of the settings that is used to retrieve it from the list of API settings.
    /// </summary>
    public string Name { get; init; } = "";

    /// <summary>
    /// The base API Url.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// The resource to ask for auth.
    /// </summary>
    public string? Resource { get; init; }

    /// <summary>
    /// The client ID used for the credential.
    /// If the API makes use of one.
    /// </summary>
    public string? ClientId { get; init; }

    /// <summary>
    /// The client secret used for the credential.
    /// If the API makes use of one.
    /// </summary>
    public string? ClientSecret { get; init; }
}
