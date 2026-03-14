using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;

public class ChatMessageApiConfiguration : IEntityTypeConfiguration<ChatMessageApi>
{
    public void Configure(EntityTypeBuilder<ChatMessageApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.HasIndex(c => c.ChatId);
        entity.Property(c => c.Message).IsRequired();
        entity.Property(c => c.SentBy).IsRequired();


        entity.HasOne(e => e.Chat)
              .WithMany(c => c.Messages)
              .HasForeignKey(e => e.ChatId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}