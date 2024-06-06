using CustomersList.Api.Constants;
using CustomersList.Api.Settings;
using CustomersList.Application.Models;
using CustomersList.Application.Services.Authentication;
using CustomersList.Application.Services.Users;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;
using YamlDotNet.Core.Tokens;

namespace CustomersList.Api.Endpoints.Authentication.Login;

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUsersService _usersService;
    private readonly IOptions<ApplicationSettings> _options;

    public LoginEndpoint( IAuthenticationService authenticationService, IUsersService usersService, IOptions<ApplicationSettings> options )
    {
        _authenticationService = authenticationService;
        _usersService = usersService;
        _options = options;
    }

    public override void Configure()
    {
        Post("/authentication/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync( LoginRequest req, CancellationToken ct )
    {
        if (await _authenticationService.CredentialsAreValidAsync(req.Email, req.Password, ct))
        {
            var user = await _usersService.GetUserByEmailAsync(req.Email);

            var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                    o.SigningKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_options.Value.Authentication.SecretKey));
                    o.ExpireAt = DateTime.UtcNow.Add(_options.Value.Authentication.TokenExpiration);
                    o.User.Roles.Add(ApplicationConstants.DefaultRole);
                    o.User.Claims.Add((ApplicationConstants.EmailClaimKey, req.Email));
                    o.User.Claims.Add((ApplicationConstants.UserIdClaimKey, user.Value.Id.ToString()));
                });

            await SendAsync(
                new
                {
                    req.Email,
                    Token = jwtToken
                });
        }
        else
            ThrowError("The supplied credentials are invalid!");
    }
}
