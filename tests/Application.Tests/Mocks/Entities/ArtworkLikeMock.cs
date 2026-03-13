using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ArtworkLikeMock
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ArtworkId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ArtworkLikeMock(
        Guid? id = null,
        Guid? userId = null,
        Guid? artworkId = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null)
    {
        Id = id ?? Guid.NewGuid();
        UserId = userId ?? Guid.NewGuid();
        ArtworkId = artworkId ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
    }

    public ArtworkLike Create()
    {
        ArtworkLike result = new ArtworkLike
        {
            Id = Id,
            UserId = UserId,
            ArtworkId = ArtworkId,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt
        };

        return result;
    }
}
