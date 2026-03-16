using Domain.Types;

namespace Domain.Entities;

public class Artwork : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public bool OnSale { get; set; }
    public Guid UserId { get; set; }
    public Guid ServiceId { get; set; }
    public string ArtworkName{ get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Artwork(Guid id, Guid userId, Guid serviceId, string artworkName, string description, string imageUrl, bool onSale, DateTime createdAt, DateTime updatedAt, DateTime? deletedAt)
    {
        Id = id;
        UserId = userId;
        ServiceId = serviceId;
        ArtworkName = artworkName;
        Description = description;
        ImageUrl = imageUrl;
        OnSale = onSale;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }

}
