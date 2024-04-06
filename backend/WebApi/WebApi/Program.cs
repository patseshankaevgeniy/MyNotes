using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Infrastructure;
using System;
using System.Threading.Tasks;

namespace WebApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();


            try
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                await db.Database.MigrateAsync();
                await SeedData.ApplySeedDataAsync(scope.ServiceProvider);
            }
            catch (Exception)
            {
                throw;
            }

            await app.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}