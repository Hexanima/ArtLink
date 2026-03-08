using Domain.Types;

namespace Domain.Entities;

public class ArtworkLike : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ArtworkId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
