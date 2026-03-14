using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ArtworkCommentApiConfiguration : IEntityTypeConfiguration<ArtworkCommentApi>
{
    public void Configure(EntityTypeBuilder<ArtworkCommentApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.ArtworkId).IsRequired();
        entity.Property(e => e.Message).IsRequired();

        entity.HasOne(e => e.User)
              .WithMany(u => u.ArtworkComments)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        
        entity.HasOne(e => e.Artwork)
              .WithMany(a => a.ArtworkComments)
              .HasForeignKey(e => e.ArtworkId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}