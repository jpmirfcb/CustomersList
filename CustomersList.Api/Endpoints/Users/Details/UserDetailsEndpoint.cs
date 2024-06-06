using CustomersList.Application.Services.Users;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Users.Details;

public class UserDetailsEndpoint : Endpoint<UserDetailsRequest, UserDetailsResponse>
{
    private readonly IUsersService _usersService;

    public UserDetailsEndpoint( IUsersService usersService )
    {
        _usersService = usersService;
    }

    public override void Configure()
    {
        Get("Users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync( UserDetailsRequest request, CancellationToken cancellationToken )
    {
        var result = await _usersService.GetUserAsync(request.Id);
        if (result.IsSuccess)
        {
            await SendAsync(new UserDetailsResponse()
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Email = result.Value.Email
            });
            return;
        }

        ThrowError("There was a problem getting the user information");
    }
}
