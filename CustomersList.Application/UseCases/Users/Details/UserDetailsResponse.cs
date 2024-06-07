namespace CustomersList.Application.UseCases.Users.Details;

public sealed record UserDetailsResponse (Guid Id, string Name, string Email);
