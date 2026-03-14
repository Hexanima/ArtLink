using Domain.Entities;

namespace Application.DTOs;

public class CreateCommissionDTO
{
    public required CommissionState State { get; set; }
    public required decimal EstablishedPrice { get; set; }
    public required string Description { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid ArtistId { get; set; }
}

public class UpdateCommissionDTO
{
    public required Guid Id { get; set; }
    public CommissionState? State { get; set; }
    public decimal? EstablishedPrice { get; set; }
    public string? Description { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? ArtistId { get; set; }
}

public class CommissionDTO
{
    public required Guid Id { get; set; }
    public required CommissionState State { get; set; }
    public required decimal EstablishedPrice { get; set; }
    public required string Description { get; set; }
    public required Guid ClientId { get; set; }
    public required Guid ArtistId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}