//using TS.Common.GSM.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace TS.Common.GSM.Extensions
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Adds Azure Key vault as configuration source
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <param name="configPrefixes">Prefixes for filtering out configurations</param>
        /// <returns></returns>
        public static IHostBuilder AddAzureKeyVaultConfiguration(this IHostBuilder hostBuilder, string[] configPrefixes)
        {
            return hostBuilder.ConfigureAppConfiguration((hostingContext, builder) =>
            {
                //builder.AddAzureKeyVault(vault: Environment.GetEnvironmentVariable(""),
                //    clientId: Environment.GetEnvironmentVariable(""),
                //    clientSecret: Environment.GetEnvironmentVariable(""),
                //    new KeyVaultSecretManager(configPrefixes));
            });
        }
    }
}
