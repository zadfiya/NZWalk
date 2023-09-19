using Microsoft.EntityFrameworkCore;
using NZWalk.API.Model;
using NZWalk.API.Model.Domain;

namespace NZWalk.API.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }

            public DbSet<Walk> Walk { get; set; }
            public DbSet<Region> Region { get; set; }
            public DbSet<Difficulty> Difficulty { get; set; }
            public DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            base.OnModelCreating(modelBuilder);
            // Entity configurations and relationships should be defined here, if needed.
            // modelBuilder.Entity<EntityType>().ToTable("TableName");
            // modelBuilder.Entity<EntityType>().HasMany(e => e.NavigationProperty);
        }
    }
}

