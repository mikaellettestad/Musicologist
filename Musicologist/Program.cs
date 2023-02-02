using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musicologist.Data;
using Musicologist.Models;

namespace Musicologist
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var iHost = CreateHostBuilder(args).Build();

            InitializeDb(iHost);

            iHost.Run();
        }

        private static void InitializeDb(IHost iHost)
        {
            using (var scope = iHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDbContext>();

                var database = new DatabaseInitializer();

                database.Initialize(context, services.GetRequiredService<UserManager<ApplicationUser>>());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}