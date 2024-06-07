using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Authentication.Login;

namespace CustomersList.Application.UseCases.Interfaces;

public interface IUserLogin : IUseCaseQuery<UserLoginRequest, UserLoginResponse, UserLoginRequestValidator>
{
}
