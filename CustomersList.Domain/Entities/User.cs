﻿using CustomersList.Domain.Abstractions.Entities;
using CustomersList.Domain.Attributes;

namespace CustomersList.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }

    [Index]
    public string Email { get; set; }

    public string Password { get; set; }

    public bool Active { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeactivatedAt { get; set; }
}
