namespace Application.DTOs;

public class CreateProvidedServiceDTO
{
    public required Guid UserId { get; set; }
    public required Guid ServiceId { get; set; }
}

public class UpdateProvidedServiceDTO
{
    public required Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ServiceId { get; set; }
}

public class ProvidedServiceDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ServiceId { get; set; }
}