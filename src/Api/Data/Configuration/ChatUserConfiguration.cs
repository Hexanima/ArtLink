using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ChatUserApiConfiguration : IEntityTypeConfiguration<ChatUserApi>
{
    public void Configure(EntityTypeBuilder<ChatUserApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.Property(c => c.ChatId).IsRequired();
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.IsAdmin).HasDefaultValue(false);

        entity.HasOne(e => e.User)
              .WithMany(u => u.ChatUsers)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);
              
        entity.HasOne(e => e.Chat)
              .WithMany(c => c.ChatUsers)
              .HasForeignKey(e => e.ChatId)
              .OnDelete(DeleteBehavior.Restrict);
              
    }
}