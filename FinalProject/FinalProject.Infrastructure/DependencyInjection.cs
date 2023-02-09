using FinalProject.Application.Common.Interfaces;
using FinalProject.Infrastructure.Context;
using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Identity.Interfaces;
using FinalProject.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<AppDbContextInitializer>();
            services.AddTransient<IUserService, UserService>();

            services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
