namespace Application.DTOs;

public class CreateServiceDTO
{
    public required string ServiceName { get; set; }
}

public class UpdateServiceDTO
{
    public required Guid Id { get; set; }
    public string? ServiceName { get; set; }
}

public class ServiceDTO
{
    public required Guid Id { get; set; }
    public required string ServiceName { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}