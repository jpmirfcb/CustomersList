using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Authentication.Login;

public sealed class UserLoginHandler : UseCaseQuery<UserLoginRequest, UserLoginResponse, UserLoginRequestValidator>, IUserLogin
{
    private readonly ILogger<UserLoginHandler> _logger;
    private readonly IUsersRepository _usersRepository;

    public UserLoginHandler( IMapper mapper, ILogger<UserLoginHandler> logger, IUsersRepository usersRepository ) : base(mapper)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    protected override async Task<Result<UserLoginResponse>> HandleAsync( UserLoginRequest request, CancellationToken ct )
    {
        try
        {
            var user = await _usersRepository.GetByEmailAsync(request.Email);
            if (user is null || user.Password != request.Password)
            {
                return Result<UserLoginResponse>.Invalid(new ValidationError("The credentials provided are invalid"));
            }

            return Result<UserLoginResponse>.Success(Mapper.Map<UserLoginResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating user credentials");
            return Result<UserLoginResponse>.Error("Error validating user credentials");
        }
    }
}
