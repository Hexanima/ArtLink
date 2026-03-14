using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class UserApiConfiguration : IEntityTypeConfiguration<UserApi>
{
    public void Configure(EntityTypeBuilder<UserApi> entity)
    {
        entity.HasMany(u => u.ProvidedServices)
              .WithOne(ps => ps.User)
              .HasForeignKey(ps => ps.UserId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(u => u.ChatInvites)
              .WithOne(ci => ci.User)
              .HasForeignKey(ci => ci.UserId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}