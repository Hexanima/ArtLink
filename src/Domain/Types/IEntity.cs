namespace Domain.Types;

public interface IEntity
{
    public Guid Id { get; set; }
}

public interface ITimestampedEntity : IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public interface ISoftDeletedEntity : IEntity
{
    public DateTime? DeletedAt { get; set; }
}
