namespace Application.DTOs;

public class CreateArtworkLikeDTO
{
    public required Guid UserId { get; set; }
    public required Guid ArtworkId { get; set; }
}

public class UpdateArtworkLikeDTO
{
    public required Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ArtworkId { get; set; }
}

public class ArtworkLikeDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ArtworkId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}