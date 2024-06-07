using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Interfaces;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class UserDetailsHandler : UseCaseQuery<UserDetailsRequest, UserDetailsResponse, UserDetailsRequestValidator>, IUserDetailsHandler
{
    private readonly IUsersRepository _usersRepository;
    private ILogger<UserDetailsHandler> _logger;

    public UserDetailsHandler(IMapper mapper, ILogger<UserDetailsHandler> logger, IUsersRepository usersRepository) : base(mapper)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    protected override async Task<Result<UserDetailsResponse>> HandleAsync(UserDetailsRequest request, CancellationToken ct)
    {
        try
        {
            var user = await _usersRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", request.Id);
                return Result<UserDetailsResponse>.NotFound();
            }

            return Result<UserDetailsResponse>.Success(Mapper.Map<UserDetailsResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while trying to get user with id {Id}", request.Id);
            return Result<UserDetailsResponse>.Error("An unexpected error occurred while getting user info");
        }

    }
}
