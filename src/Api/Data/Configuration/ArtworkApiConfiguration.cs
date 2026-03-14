using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ArtworkApiConfiguration : IEntityTypeConfiguration<ArtworkApi>
    {
        public void Configure(EntityTypeBuilder<ArtworkApi> entity)
        {
            entity.HasIndex(c => c.Id);
            entity.Property(e => e.ServiceId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.OnSale).IsRequired();
            entity.Property(e => e.ArtworkName).IsRequired();
            entity.Property(e => e.ImageUrl).IsRequired();

            entity.HasOne(e => e.User)
                  .WithMany(u => u.ArtworkApis)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
