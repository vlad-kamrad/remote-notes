//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RN.Application.Common.Interfaces;
using RN.Infrastructure.Identity;
using RN.Infrastructure.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RN.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddTransient<IDateTime, DateTimeService>();

            // TODO: Change this
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RN.DB;Trusted_Connection=True;MultipleActiveResultSets=true"
                , b => b.MigrationsAssembly("RN.Infrastructure"))
              //b => b.MigrationsAssembly(typeof(IApplicationDbContext).Assembly.FullName))
              );

            //services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            /*     services.AddEntityFrameworkSqlServer()
                     .AddDbContext<ApplicationDbContext>(pr =>
                     pr.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));*/
            //(localdb)\MSSQLLocalDB
            /* services.AddEntityFrameworkSqlServer().AddDbContext<IApplicationDbContext, ApplicationDbContext>((provider, options) =>
             {
                 options
                 .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RN.DB;Trusted_Connection=True;MultipleActiveResultSets=true")
                 ;//.UseInternalServiceProvider(provider)
                 //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
             });*/


            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService(typeof(ApplicationDbContext)));
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
           // services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            // Disable default props ASP.Identity
            /*services.AddDefaultIdentity<ApplicationUser>(x =>
            {
                // TODO: Use separate middleware
                x.Password.RequiredLength = 5;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireDigit = false;
            }).AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            // Here
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                            {
                                context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };

                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("keyaisjdoasijdioasjdoiasjdoisajd")),
                        ValidIssuer = "123",
                        ValidAudience = "123",
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
