using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ServiceMock
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ServiceMock(
        Guid? id = null,
        string? serviceName = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null)
    {
        Id = id ?? Guid.NewGuid();
        ServiceName = serviceName ?? "Illustration";
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
    }

    public Service Create()
    {
        Service result = new Service
        {
            Id = Id,
            ServiceName = ServiceName,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt
        };

        return result;
    }
}
