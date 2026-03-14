namespace Api.ApiEntities;

public class ArtworkCommentApi
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserApi User { get; set; } = null!;
    public Guid ArtworkId { get; set; }
    public ArtworkApi Artwork { get; set; } = null!;
    public required string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
