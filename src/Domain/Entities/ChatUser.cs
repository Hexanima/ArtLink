using Domain.Types;

namespace Domain.Entities;

public class ChatUser : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public bool IsAdmin { get; set; } = false;
}
