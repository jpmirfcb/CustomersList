using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Application.UseCases.Users.CreateUser;
using CustomersList.Api.Extensions;
using FastEndpoints;

namespace CustomersList.Api.Endpoints.Users;

public class CreateUserEndpoint : Endpoint<CreateUserRequest>
{

    private readonly ICreateUser _createUser;

    public CreateUserEndpoint( ICreateUser createUser )
    {
        _createUser = createUser;
    }

    public override void Configure()
    {
        Post("Users");
        AllowAnonymous();
    }

    public override async Task HandleAsync( CreateUserRequest request, CancellationToken cancellationToken )
    {

        var result = await _createUser.ExecuteAsync(request, cancellationToken);
        if (result.IsSuccess)
        {
            await SendCreatedAtAsync<UserDetailsEndpoint>(new { result.Value.Id }, result.Value);
        }
        else
        {
            await this.SendResultErrors(result, cancellationToken);
        }
    }
}
