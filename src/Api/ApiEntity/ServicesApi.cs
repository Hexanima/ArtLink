namespace Api.ApiEntities;

public class ServiceApi
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public ICollection<ArtworkApi> ArtworkApis { get; set; } = new List<ArtworkApi>();
    public ICollection<ProvidedServiceApi> ProvidedServices { get; set; } = new List<ProvidedServiceApi>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
