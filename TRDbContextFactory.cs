using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


//this was made so that I can create controllers, wouldn't let me before
namespace TruckRecords.Models
{
    public class TRDbContextFactory : IDesignTimeDbContextFactory<TRDbContext>
    {
        public TRDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TRDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new TRDbContext(builder.Options);
        }
    }
}
