using CustomersList.Api.Constants;
using CustomersList.Api.Settings;
using CustomersList.Application.UseCases.Authentication.Login;
using CustomersList.Application.UseCases.Interfaces;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;

namespace CustomersList.Api.Endpoints.Authentication;

public class LoginEndpoint : Endpoint<UserLoginRequest>
{
    private readonly IUserLogin _userLogin;
    private readonly IOptions<ApplicationSettings> _options;

    public LoginEndpoint(IUserLogin userLogin, IOptions<ApplicationSettings> options)
    {
        _userLogin = userLogin;
        _options = options;
    }

    public override void Configure()
    {
        Post("/authentication/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
    {
        var result = await _userLogin.ExecuteAsync(req, ct);
        if (result.IsSuccess)
        {
            var user = result.Value;

            var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_options.Value.Authentication.SecretKey));
                o.ExpireAt = DateTime.UtcNow.Add(_options.Value.Authentication.TokenExpiration);
                o.User.Roles.Add(ApplicationConstants.DefaultRole);
                o.User.Claims.Add((ApplicationConstants.EmailClaimKey, req.Email));
                o.User.Claims.Add((ApplicationConstants.UserIdClaimKey, user.Id.ToString()));
            });

            await SendAsync(
                new
                {
                    user.Id,
                    user.Email,
                    Token = jwtToken
                });
        }
        else
            ThrowError("The supplied credentials are invalid!");
    }
}
