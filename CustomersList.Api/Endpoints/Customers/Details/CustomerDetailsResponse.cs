namespace CustomersList.Api.Endpoints.Customers.Details;

public class CustomerDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}