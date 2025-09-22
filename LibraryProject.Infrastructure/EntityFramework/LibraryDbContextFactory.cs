using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibraryProject.Infrastructure.EntityFramework
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryProjectDbContext>
    {
        public LibraryProjectDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../LibraryProject.API");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<LibraryProjectDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new LibraryProjectDbContext(optionsBuilder.Options);
        }
    }
}
