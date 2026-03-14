namespace Application.DTOs;

public class CreateArtworkCommentDTO
{
    public required Guid UserId { get; set; }
    public required Guid ArtworkId { get; set; }
    public required string Message { get; set; }
}

public class UpdateArtworkCommentDTO
{
    public required Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ArtworkId { get; set; }
    public string? Message { get; set; }
}

public class ArtworkCommentDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ArtworkId { get; set; }
    public required string Message { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}