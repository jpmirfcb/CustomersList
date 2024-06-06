using Ardalis.Result;
using CustomersList.Application.DTO;
using CustomersList.Domain.Entities;

namespace CustomersList.Application.Services.Users;

public interface IUsersService 
{
    Task<Result<User>> CreateUserAsync(CreateUserDTO dto);
    Task<Result> SetActiveUserAsync(Guid id, bool active);
    Task<Result<User>> GetUserAsync(Guid id);
    Task<Result<User>> GetUserByEmailAsync(string email);
    Task<Result<User>> UpdateUserAsync(Guid id, UserDTO dto);
}
