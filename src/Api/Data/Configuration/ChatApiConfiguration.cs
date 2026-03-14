using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ChatApiConfiguration : IEntityTypeConfiguration<ChatApi>
{
    public void Configure(EntityTypeBuilder<ChatApi> entity)
    {

            entity.HasIndex(c => c.Id);
            entity.Property(c => c.Title).IsRequired();

        entity.HasMany(c => c.Messages)
              .WithOne(m => m.Chat)
              .HasForeignKey(m => m.ChatId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(c => c.ChatUsers)
              .WithOne(u => u.Chat)
              .HasForeignKey(u => u.ChatId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(c => c.Invites)
              .WithOne(i => i.Chat)
                .HasForeignKey(i => i.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}