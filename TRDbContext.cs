using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;

namespace TruckRecords.Models
{
    public class TRDbContext : DbContext
    {
        public TRDbContext(DbContextOptions<TRDbContext> options)
            : base(options)
        {
        }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<BuildRecord> BuildRecords { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TruckTest> TruckTests { get; set; }


        // Override the OnModelCreating method to use the Fluent API for further configuration
        //Also ignoring select list item so it can make controller
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //making random test result id
            modelBuilder.Entity<TestResult>()
            .HasKey(t => t.TestResultID);

            modelBuilder.Entity<TestResult>()
                .Property(t => t.TestResultID)
                .ValueGeneratedOnAdd();

            // Define foreign key relationships
            modelBuilder.Entity<BuildRecord>()
                .HasOne<Truck>(br => br.Truck)
                .WithMany(t => t.BuildRecords)
                .HasForeignKey(br => br.TruckID);

            modelBuilder.Entity<BuildRecord>()
                .HasOne<Component>(br => br.Component)
                .WithMany(c => c.BuildRecords)
                .HasForeignKey(br => br.ComponentID);

            modelBuilder.Ignore<SelectListItem>();

            // Additional configurations can be added here as needed
        }
        public DbSet<TruckRecords.Models.TruckTest> TruckTest { get; set; } = default!;
    }
}
