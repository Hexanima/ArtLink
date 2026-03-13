using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ProvidedServiceMock
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ServiceId { get; set; }

    public ProvidedServiceMock(
        Guid? id = null,
        Guid? userId = null,
        Guid? serviceId = null)
    {
        Id = id ?? Guid.NewGuid();
        UserId = userId ?? Guid.NewGuid();
        ServiceId = serviceId ?? Guid.NewGuid();
    }

    public ProvidedService Create()
    {
        ProvidedService result = new ProvidedService
        {
            Id = Id,
            UserId = UserId,
            ServiceId = ServiceId
        };

        return result;
    }
}
