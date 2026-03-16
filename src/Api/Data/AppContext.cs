using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ArtworkApi> ArtworkApi { get; set; }
        public DbSet<ArtworkCommentApi> ArtworkCommentApi { get; set; }
        public DbSet<ArtworkLikeApi> ArtworkLikeApi { get; set; }
        public DbSet<ChatApi> ChatApi { get; set; }
        public DbSet<ChatInviteApi> ChatInviteApi { get; set; }
        public DbSet<ChatMessageApi> ChatMessageApi { get; set; }
        public DbSet<ChatUserApi> ChatUserApi { get; set; }
        public DbSet<ProvidedServiceApi> ProvidedServiceApi { get; set; }
        public DbSet<ServiceApi> ServiceApi { get; set; }
        public DbSet<UserApi> UserApi { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}