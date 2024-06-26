using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TS.Common.Authentication;

namespace TS.Common.PubSub
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all required services for Azure Data Factory integration.
        /// </summary>
        public static IServiceCollection AddPubSubClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenticationClient(configuration);
            services.AddTransient<IPubSubClient, PubSubClient>();

            return services;
        }

    }
}