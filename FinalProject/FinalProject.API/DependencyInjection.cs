using AutoMapper;
using FinalProject.Application.Common.Mapping;
using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection

{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>();
            // Add services to the container.

            services.AddAuthentication(i =>
            {
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(i =>
            {
                i.RequireHttpsMetadata = false;
                i.SaveToken = true;
                i.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                i.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/demohub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                  JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                  defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            

            var mappConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new ApplicationMappingProfile());
                x.AddProfile(new InfrastructureMappingProfile());
            });

            IMapper mapper = mappConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddCors(x =>
            {
                x.AddPolicy("DefaultPolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173", "http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers();

            return services;
        }
    }
}
