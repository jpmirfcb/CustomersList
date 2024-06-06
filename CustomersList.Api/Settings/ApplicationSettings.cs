
namespace CustomersList.Api.Settings;

public class ApplicationSettings
{
    public AuthenticationSettings Authentication { get; set; } = new AuthenticationSettings();
}

public class AuthenticationSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public TimeSpan TokenExpiration { get; set; } = TimeSpan.FromHours(1);
}
