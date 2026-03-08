using Domain.Types;

public class User : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }

    public SecureUser ToSecureUser()
    {
        return new()
        {
            Id = Id,
            Email = Email,
            UserName = UserName,
            CreatedAt = CreatedAt,
            DeletedAt = DeletedAt,
            UpdatedAt = UpdatedAt
        };
    }
}

public class SecureUser : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
}
