using BlocketLiteEFCoreDB.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace BlocketLiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            var host = CreateHostBuilder(args).Build();
            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var environment = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                    if (environment == "Development")
                    {
                        var context = scope.ServiceProvider.GetService<BlocketLiteContext>();
                        // for demo purposes, delete the database & migrate on startup so 
                        // we can start with a clean slate
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    }
                    if (environment == "Production")
                    {
                        var context = scope.ServiceProvider.GetService<BlocketLiteContext>();
                        // For production we need to know that the DB is created
                        context.Database.EnsureCreated();
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }           
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
