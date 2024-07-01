using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;
using TS.Common.Constants;

namespace TS.Common.Authentication
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all required services for Azure Data Factory integration.
        /// </summary>
        public static IServiceCollection AddAuthenticationClient(this IServiceCollection services, IConfiguration configuration)
        {
            var gcpConfig = configuration.GetSection(nameof(GcpConfiguration)).Get<GcpConfiguration>();

            services.AddTransient<IGcpConfiguration>(_ => gcpConfig);
            services.AddTransient<IAuthenticationClient, AuthenticationClient>();

            return services;
        }

        /// <summary>
        /// Add JWT Authentication to access the resource
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="logger">Logger</param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration[Auth.JwtIssuer],
                ValidAudiences = configuration.GetSection(Auth.JwtAudience).Get<List<string>>(),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Auth.JwtKey]))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = afc =>
                {
                    logger.LogError(afc?.Exception?.Message);
                    return Task.CompletedTask;
                }
            };
        });
            return services;
        }

    }
}
