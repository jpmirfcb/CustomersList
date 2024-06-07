using CustomersList.Application.Services.Users;
using CustomersList.Application.UseCases.Users.Details;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Users;

public class UserDetailsEndpoint : Endpoint<DetailsUserRequest, DetailsUserResponse>
{
    private readonly IUsersService _usersService;

    public UserDetailsEndpoint(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public override void Configure()
    {
        Get("Users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DetailsUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _usersService.GetUserAsync(request.Id);
        if (result.IsSuccess)
        {
            await SendAsync(new DetailsUserResponse()
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
