using Api.ApiEntities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppContext : DbContext
    {
        public DbSet<ArtworkApi> ArtworkApi { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }
    }
}