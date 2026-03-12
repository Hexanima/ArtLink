
using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;
public class AppContext : DbContext
{
    public DbSet<Artwork> Artworks { get; set; }

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }

    public DbSet<ArtworkApi> ArtworkApi { get; set; }



protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ArtworkApi>(entity =>
    {
        entity.HasIndex(c => c.Id);
        entity.Property(e => e.ServiceId).IsRequired();
        entity.Property(e => e.UserId).IsRequired();
        entity.Property(e => e.OnSale).IsRequired();
        entity.Property(e => e.ArtworkName).IsRequired();
        entity.Property(e => e.ImageUrl).IsRequired();


            
              // Relation
        entity.HasOne(e => e.User)
        .WithMany(u => u.ArtworkApis)
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Restrict);
    });

}
    

   

    
}