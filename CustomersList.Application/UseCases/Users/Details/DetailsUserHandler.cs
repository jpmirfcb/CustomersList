using Ardalis.Result;
using AutoMapper;
using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Domain.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.UseCases.Users.Details;

public sealed class DetailsUserHandler : UseCaseQuery<DetailsUserRequest, DetailsUserResponse, DetailsUserRequestValidator>
{
    private readonly IUsersRepository _usersRepository;
    private ILogger<DetailsUserHandler> _logger;

    public DetailsUserHandler(IMapper mapper, ILogger<DetailsUserHandler> logger, IUsersRepository usersRepository) : base(mapper)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    protected override async Task<Result<DetailsUserResponse>> HandleAsync(DetailsUserRequest request, CancellationToken ct)
    {
        try
        {
            var user = await _usersRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", request.Id);
                return Result<DetailsUserResponse>.NotFound();
            }

            return Result<DetailsUserResponse>.Success(Mapper.Map<DetailsUserResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while trying to get user with id {Id}", request.Id);
            return Result<DetailsUserResponse>.Error("An unexpected error occurred while getting user info");
        }

    }
}
