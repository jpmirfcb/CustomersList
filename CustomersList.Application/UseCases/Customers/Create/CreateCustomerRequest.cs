namespace CustomersList.Application.UseCases.Customers.Create;

public sealed record CreateCustomerRequest(string Name, string Email, string Phone);
