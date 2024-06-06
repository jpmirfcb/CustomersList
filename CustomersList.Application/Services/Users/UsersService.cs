using Ardalis.Result;
using CustomersList.Application.DTO;
using CustomersList.Application.Repositories;
using CustomersList.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomersList.Application.Services.Users;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<UsersService> _logger;

    public UsersService( IUsersRepository usersRepository, ILogger<UsersService> logger )
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    public async Task<Result<User>> CreateUserAsync( CreateUserDTO dto )
    {
        try
        {

            var entity = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                DeactivatedAt = null,
                Id = Guid.NewGuid(),
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _usersRepository.CreateAsync(entity);

            return Result<User>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return Result<User>.Error("Error creating user");
        }
    }

    public async Task<Result<User>> GetUserAsync( Guid id )
    {
        try
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return Result<User>.NotFound($"User with id {id} not found");
            }

            return Result<User>.Success(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by id");
            return Result<User>.Error("Error getting user by id");
        }
    }

    public async Task<Result<User>> GetUserByEmailAsync( string email )
    {
        try
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user is null)
            {
                return Result<User>.NotFound($"User with email {email} not found");
            }
            else
            {
                return Result<User>.Success(user);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user by id");
            return Result<User>.Error("Error getting user by id");
        }
    }

    public async Task<Result> SetActiveUserAsync( Guid id, bool active )
    {
        try
        {
            var existingUser = await _usersRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return Result.NotFound($"User with id {id} not found");
            }
            existingUser.Active = active;

            await _usersRepository.UpdateAsync(existingUser, id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error setting user's active state to {active}");
            return Result.Error($"Error setting user's active state to {active}");
        }
    }

    public async Task<Result<User>> UpdateUserAsync( Guid id, UserDTO dto )
    {
        try
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return Result<User>.NotFound($"User with id {id} not found");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _usersRepository.UpdateAsync(user, id);
            return Result.Success(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating users data");
            return Result<User>.Error("Error updating users data");
        }
    }
}
