using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ProvidedServiceApiConfiguration : IEntityTypeConfiguration<ProvidedServiceApi>
{
    public void Configure(EntityTypeBuilder<ProvidedServiceApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.ServiceId).IsRequired();

        entity.HasOne(e => e.User)
              .WithMany(u => u.ProvidedServices)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Service)
              .WithMany(s => s.ProvidedServices)
              .HasForeignKey(e => e.ServiceId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}