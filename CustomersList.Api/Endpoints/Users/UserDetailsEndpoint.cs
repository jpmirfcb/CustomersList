using CustomersList.Api.Extensions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Application.UseCases.Users.Details;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Users;

public class UserDetailsEndpoint : Endpoint<UserDetailsRequest, UserDetailsResponse>
{
    private readonly IUserDetailsHandler _usersDetailsHandler;

    public UserDetailsEndpoint( IUserDetailsHandler usersDetailsHandler )
    {
        _usersDetailsHandler = usersDetailsHandler;
    }

    public override void Configure()
    {
        Get("Users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync( UserDetailsRequest request, CancellationToken cancellationToken )
    {

        var result = await _usersDetailsHandler.ExecuteAsync(request, cancellationToken);
        if (result.IsSuccess)
        {
            await SendAsync(result.Value);
            return;
        }

        await this.SendResultErrors(result, cancellationToken);
    }
}
