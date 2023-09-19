using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Model;
using System.Text;

namespace NZWalk.API.Data
{
    public class AddAuthDbContext : IdentityDbContext<IdentityUser>
    {
        
        public AddAuthDbContext() {
         
            
        }
        public AddAuthDbContext(DbContextOptions<AddAuthDbContext> options): base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            var readerRoleId = new Guid(Guid.NewGuid().ToString());
            var writterRoleId = new Guid(Guid.NewGuid().ToString());
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id  = readerRoleId.ToString(),
                    ConcurrencyStamp = readerRoleId.ToString(),
                    Name="Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id  = writterRoleId.ToString(),
                    ConcurrencyStamp = writterRoleId.ToString(),
                    Name="Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);




        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                

                var connectionString = configuration
                            .GetConnectionString("DBConnectionAuth");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
