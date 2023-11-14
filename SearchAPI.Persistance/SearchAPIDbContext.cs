using SearchAPI.Domain.Common;
using SearchAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SearchAPI.Persistance.Helper;

namespace SearchAPI.Persistance
{
    public class SearchAPIDbContext : DbContext
    {
        public DbSet<Rectangle> Rectangles { get; set; }

        public SearchAPIDbContext(DbContextOptions<SearchAPIDbContext> options) : base(options)
        {
        }

        public SearchAPIDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseNpgsql(@"Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure spatial data support
            modelBuilder.HasPostgresExtension("postgis");

            // Seed data
            modelBuilder.Entity<Rectangle>().HasData(SeedData.GetSeedRectangles());

            // Add spatial index for efficient spatial queries
            modelBuilder.Entity<Rectangle>().HasIndex(r => r.Geometry).HasMethod("gist");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in this.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
