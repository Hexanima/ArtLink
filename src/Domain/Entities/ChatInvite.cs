using Domain.Types;

namespace Domain.Entities;

public class ChatInvite : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public Guid SentBy { get; set; }
}
