using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteNotes;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.Notes.Queries;
using RN.Application.UseCases.User.Commands.LoginUser;
using RN.Infrastructure;
using System;
using System.Threading.Tasks;

namespace RN.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          //  CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
           
            using (var scope = host.Services.CreateScope()) {
                try {
                    var services = scope.ServiceProvider;
                    //var context = services.GetRequiredService<ApplicationDbContext>();
                    //context.Database.Migrate();

                    //  var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                //  await ApplicationDbContextSeed.SeedAsync(userManager);
                } catch (Exception ex) { }
            }
           await host.RunAsync();
         //   await host.RunAsync();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                    .UseDefaultServiceProvider(options => options.ValidateScopes = false);
            });
    }
}
