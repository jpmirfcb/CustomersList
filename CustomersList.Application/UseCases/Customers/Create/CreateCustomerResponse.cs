namespace CustomersList.Application.UseCases.Customers.Create;

public sealed class CreateCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
