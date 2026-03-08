using Domain.Types;

namespace Domain.Entities;

public class ProvidedService : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId;
    public Guid ServiceId;
}
