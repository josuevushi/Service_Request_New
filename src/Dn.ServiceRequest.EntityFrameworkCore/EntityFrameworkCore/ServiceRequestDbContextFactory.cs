using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dn.ServiceRequest.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ServiceRequestDbContextFactory : IDesignTimeDbContextFactory<ServiceRequestDbContext>
{
    public ServiceRequestDbContext CreateDbContext(string[] args)
    {
        ServiceRequestEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ServiceRequestDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ServiceRequestDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Dn.ServiceRequest.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
