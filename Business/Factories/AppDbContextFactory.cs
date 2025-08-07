using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BuzzShopping.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace Business.Factories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory();
                var projectRoot = Path.GetFullPath(Path.Combine(basePath, "..", "BuzzShopping"));


                Console.WriteLine($"Base path: {basePath}");
                Console.WriteLine($"Project root path: {projectRoot}");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(projectRoot)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("sqlserver");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string 'sqlserver' is null or empty. Please check your appsettings.json.");
                }

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new AppDbContext(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando DbContext: {ex.Message}");
                throw;
            }
        }
    }
}
