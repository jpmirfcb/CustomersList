using CustomersList.Domain.Abstractions.Entities;

namespace CustomersList.Domain.Entities;

public class Customer : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
