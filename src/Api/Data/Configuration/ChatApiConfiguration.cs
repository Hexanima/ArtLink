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
    }
}