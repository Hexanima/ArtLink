using Api.ApiEntities;
using Domain.Entities;
using DataAppContext = Api.Data.AppContext;

namespace Api.Services;

public class PostgreArtworkService : EfCoreCrudService<Artwork, ArtworkApi>
{
    public PostgreArtworkService(DataAppContext context) : base(context)
    {
    }

    protected override Guid GetApiId(ArtworkApi apiEntity)
    {
        return apiEntity.Id;
    }

    protected override void SetApiId(ArtworkApi apiEntity, Guid id)
    {
        apiEntity.Id = id;
    }

    protected override Artwork ToDomain(ArtworkApi apiEntity)
    {
        return new Artwork
        {
            Id = apiEntity.Id,
            OnSale = apiEntity.OnSale,
            UserId = apiEntity.UserId,
            ServiceId = apiEntity.ServiceId,
            ArtworkName = apiEntity.ArtworkName,
            Description = apiEntity.Description,
            ImageUrl = apiEntity.ImageUrl,
            CreatedAt = apiEntity.CreatedAt,
            UpdatedAt = apiEntity.UpdatedAt,
            DeletedAt = apiEntity.DeletedAt
        };
    }

    protected override ArtworkApi ToApi(Artwork domainEntity)
    {
        var now = DateTime.UtcNow;
        return new ArtworkApi
        {
            Id = domainEntity.Id,
            OnSale = domainEntity.OnSale,
            UserId = domainEntity.UserId,
            ServiceId = domainEntity.ServiceId,
            ArtworkName = domainEntity.ArtworkName,
            Description = domainEntity.Description,
            ImageUrl = domainEntity.ImageUrl,
            CreatedAt = domainEntity.CreatedAt == default ? now : domainEntity.CreatedAt,
            UpdatedAt = domainEntity.UpdatedAt == default ? now : domainEntity.UpdatedAt,
            DeletedAt = domainEntity.DeletedAt
        }
        ;
    }

    protected override void MapForUpdate(ArtworkApi target, Artwork source)
    {
        target.OnSale = source.OnSale;
        target.UserId = source.UserId;
        target.ServiceId = source.ServiceId;
        target.ArtworkName = source.ArtworkName;
        target.Description = source.Description;
        target.ImageUrl = source.ImageUrl;
        target.DeletedAt = source.DeletedAt;
        target.UpdatedAt = DateTime.UtcNow;
    }
}