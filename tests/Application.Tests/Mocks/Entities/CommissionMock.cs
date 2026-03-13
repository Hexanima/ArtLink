using Domain.Entities;

namespace Tests.Mocks.Entities;
public class CommissionMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public CommissionState State { get; set; }
    public decimal EstablishedPrice { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public Guid ArtistId { get; set; }

    public CommissionMock(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        CommissionState? state = null,
        decimal? establishedPrice = null,
        string? description = null,
        Guid? clientId = null,
        Guid? artistId = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
        State = state ?? CommissionState.Pending;
        EstablishedPrice = establishedPrice ?? 150m;
        Description = description ?? "Commission request";
        ClientId = clientId ?? Guid.NewGuid();
        ArtistId = artistId ?? Guid.NewGuid();
    }

    public Commission Create()
    {
        Commission result = new Commission
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            State = State,
            EstablishedPrice = EstablishedPrice,
            Description = Description,
            ClientId = ClientId,
            ArtistId = ArtistId
        };

        return result;
    }
}
