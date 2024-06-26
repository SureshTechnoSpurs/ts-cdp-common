using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;

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
    }
}
