using CustomersList.Application.UseCases.Abstractions;
using CustomersList.Application.UseCases.Users.Details;

namespace CustomersList.Application.UseCases.Interfaces;

public interface IUserDetailsHandler : IUseCaseQuery<UserDetailsRequest, UserDetailsResponse, UserDetailsRequestValidator>
{
}
