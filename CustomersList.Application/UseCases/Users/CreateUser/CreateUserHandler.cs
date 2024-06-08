using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using CustomersList.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Users.CreateUser;

public sealed class CreateUserHandler : UseCaseQuery<CreateUserRequest, CreateUserResponse, CreateUserRequestValidator>, ICreateUser
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IUsersRepository _usersRepository;

    public CreateUserHandler(IMapper mapper, IUsersRepository usersRepository, ILogger<CreateUserHandler> logger) : base(mapper)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    protected override async Task<Result<CreateUserResponse>> HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        try
        {
            var user = Mapper.Map<User>(request);


            var result = await _usersRepository.CreateAsync(user);

            if (result is not null)
            {
                var response = Mapper.Map<CreateUserResponse>(result);

                return Result<CreateUserResponse>.Created(response, $"users/{response.Id}");

            }

            return Result<CreateUserResponse>.Error("Error creating user");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return Result<CreateUserResponse>.Error("Error creating user");
        }
    }
}
