using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ServiceApiConfiguration : IEntityTypeConfiguration<ServiceApi>
{
    public void Configure(EntityTypeBuilder<ServiceApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.Property(e => e.ServiceName).IsRequired();
        
        
       }
}