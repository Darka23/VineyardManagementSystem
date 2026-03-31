using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Vineyard> Vineyards { get; set; }
        public DbSet<GrapeVariety> GrapeVarieties { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<ClimateLog> ClimateLogs { get; set; }
        public DbSet<FieldActivity> FieldActivities { get; set; }
        public DbSet<Harvest> Harvests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FieldActivity>()
                .Property(f => f.Cost)
                .HasPrecision(18, 2);
        }
    }

}
