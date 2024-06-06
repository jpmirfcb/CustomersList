namespace CustomersList.Domain.Abstractions.Entities;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; }
}
