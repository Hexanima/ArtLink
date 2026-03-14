namespace Application.DTOs;

public class CreateArtworkDTO
{
    public required Guid UserId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string ArtworkName { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public bool OnSale { get; set; }
}

public class UpdateArtworkDTO
{
    public required Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ServiceId { get; set; }
    public string? ArtworkName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool? OnSale { get; set; }
}

public class ArtworkDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string ArtworkName { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required bool OnSale { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}