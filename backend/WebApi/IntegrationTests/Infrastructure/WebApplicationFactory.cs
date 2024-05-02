using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Persistence;
using System.Data;
using System.Data.Common;
using WebApi;

namespace IntegrationTests.Infrastructure;

public sealed class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private DbConnection _dbConnection = default!;

    protected override IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }

    public Action<IServiceCollection> ConfigureServices { get; set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove SqlServer
            services.RemoveAll(typeof(ApplicationDbContext));
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add SqlLite
            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlite(CreateInMemoryDatabase());
                });

            ConfigureServices?.Invoke(services);
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (_dbConnection.State != ConnectionState.Closed)
        {
            _dbConnection.Close();
        }

        base.Dispose(disposing);
    }

    private DbConnection CreateInMemoryDatabase()
    {
        if (_dbConnection == null)
        {
            _dbConnection = new SqliteConnection("DataSource=:memory:");
            _dbConnection.Open();
        }

        return _dbConnection;
    }
}