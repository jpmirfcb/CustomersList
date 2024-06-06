
using CustomersList.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService( IUsersRepository usersRepository, ILogger<AuthenticationService> logger )
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    public async Task<bool> CredentialsAreValidAsync( string email, string password, CancellationToken ct )
    {
        try
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            if(user is null)
            {
                return false;
            }

            return user.Password == password;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating user credentials");
            return false;
        }
    }
}
