namespace CustomersList.Application.Models;

public class UpdateCustomerRequest
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
}