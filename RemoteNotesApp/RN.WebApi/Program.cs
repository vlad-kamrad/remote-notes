using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteNotes;
using System;
using System.Threading.Tasks;

namespace RN.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();

            // Use for data migrations
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // var services = scope.ServiceProvider;
                    // var context = services.GetRequiredService<ApplicationDbContext>();
                    // context.Database.Migrate();
                    // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    // await ApplicationDbContextSeed.SeedAsync(userManager);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Migration Error \n{ex.Message} \n{ex.StackTrace}");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                    .UseDefaultServiceProvider(options => options.ValidateScopes = false);
            });
    }
}
