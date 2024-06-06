using CustomersList.Api.Endpoints.Users.Details;
using CustomersList.Application.DTO;
using FastEndpoints;
using CustomersList.Application.Services.Users;
using CustomersList.Application.Models;
using CustomersList.Api.Middleware;

namespace CustomersList.Api.Endpoints.Users.Create;

public class CreateUserEndpoint : Endpoint<CreateUserRequest>
{
    private readonly IUsersService _userService;

    public CreateUserEndpoint( IUsersService customerRepository )
    {
        _userService = customerRepository;
    }

    public override void Configure()
    {
        Post("Users");
        AllowAnonymous();
        PreProcessor<ValidationsPreprocessor<CreateUserRequest>>();
    }

    public override async Task HandleAsync( CreateUserRequest request, CancellationToken cancellationToken )
    {
        var dto = new CreateUserDTO()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };

        var result = await _userService.CreateUserAsync(dto);


        if (result.IsSuccess)
        {
            await SendCreatedAtAsync<UserDetailsEndpoint>(new { Id = result.Value.Id }, new CreateUserResponse()
            {
                Email = result.Value.Email,
                Id = result.Value.Id,
                Name = result.Value.Name
            });
        }
        else
        {
            ThrowError(result.Errors.First());
        }
    }
}
