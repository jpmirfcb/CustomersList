namespace CustomersList.Application.UseCases.Customers.Details;

public sealed record CustomerDetailsResponse(Guid Id, string Name, string Email, string Phone);
