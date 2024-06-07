using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Users.CreateUser;

namespace CustomersList.Application.UseCases.Interfaces;

public interface ICreateUser : IUseCaseQuery<CreateUserRequest, CreateUserResponse, CreateUserRequestValidator>
{
}
