using Domain.Types;

namespace Domain.Entities;

public enum CommissionState
{
    Pending,
    Rejected,
    Payed
}

public class Commission : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public CommissionState State { get; init; }
    public decimal EstablishedPrice { get; init; }
    public required string Description { get; set; }
    public Guid ClientId { get; init; }
    public Guid ArtistId { get; init; }
}
