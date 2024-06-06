
namespace CustomersList.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<bool> CredentialsAreValidAsync( string email, string password, CancellationToken ct );
}