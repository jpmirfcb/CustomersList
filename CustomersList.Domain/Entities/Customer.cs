using CustomersList.Domain.Abstractions.Entities;
using CustomersList.Domain.Attributes;

namespace CustomersList.Domain.Entities;

public class Customer : EntityBase
{
    public string Name { get; set; }

    [Index]
    public string Email { get; set; }

    public string Phone { get; set; }
}
