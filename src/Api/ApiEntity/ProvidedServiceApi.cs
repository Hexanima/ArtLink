namespace Api.ApiEntities;

public class ProvidedServiceApi
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserApi User { get; set; } = null!;
    public Guid ServiceId { get; set; }
    public ServiceApi Service { get; set; } = null!;
}
