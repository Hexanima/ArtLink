using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Configuration;
public class ChatInviteApiConfiguration : IEntityTypeConfiguration<ChatInviteApi>
{
    public void Configure(EntityTypeBuilder<ChatInviteApi> entity)
    {
        entity.HasIndex(c => c.Id);
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.ChatId).IsRequired();
        entity.Property(e => e.SentBy).IsRequired();


        entity.HasOne(e => e.User)
              .WithMany(u => u.ChatInvites)
              .HasForeignKey(e => e.UserId)
              .OnDelete(DeleteBehavior.Restrict);
              
        entity.HasOne(e => e.Chat)
              .WithMany(c => c.Invites)
              .HasForeignKey(e => e.ChatId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}