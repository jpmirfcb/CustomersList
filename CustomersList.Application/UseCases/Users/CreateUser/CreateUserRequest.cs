namespace CustomersList.Application.UseCases.Users.CreateUser;

public sealed record CreateUserRequest(string Email, string Password, string Name);