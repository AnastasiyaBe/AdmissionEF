using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AdmissionEF.Models
{
    public class AdmissionContextFactory : IDesignTimeDbContextFactory<AdmissionContext>
    {
        public AdmissionContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AdmissionContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AdmissionContext(optionsBuilder.Options);
        }
    }
}