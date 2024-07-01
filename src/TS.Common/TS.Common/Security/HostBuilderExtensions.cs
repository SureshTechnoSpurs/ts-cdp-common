using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using Google.Protobuf;
using System.Text;
using System;

namespace TS.Common.Security
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddGcpConfiguration(this IHostBuilder hostBuilder, string configPrefix)
        {
            string configPrefix2 = configPrefix;
            return hostBuilder.ConfigureAppConfiguration(delegate (HostBuilderContext hostingContext, IConfigurationBuilder config)
            {
                // Read connection string from Secret Manager (replace with your Secret Manager path)
                var connectionString = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_SECRET") + "/projects/your-project-id/secrets/app-config/versions/latest";
                var secretManager = SecretManagerServiceClient.Create();
                var accessSecretVersionRequest = new AccessSecretVersionRequest { Name = connectionString };
                var secretVersion = secretManager.AccessSecretVersion(accessSecretVersionRequest);
                var payloadBytes = secretVersion.Payload.ToByteArray();
                //var payload = Encoding.UTF8.GetString(payloadBytes);
                var payload = new MemoryStream(payloadBytes);

                // Load configuration from environment variables with the prefix
                config.AddEnvironmentVariables(prefix: configPrefix2);

                // Optionally parse JSON payload from Secret Manager
                if (payloadBytes.Any())
                {
                    config.AddJsonStream(payload);
                }
            });
        }

        public static IHostBuilder AddGcpSecretManagerConfiguration(this IHostBuilder hostBuilder, string[] configPrefixes)
        {
            return hostBuilder.ConfigureAppConfiguration(delegate (HostBuilderContext hostingContext, IConfigurationBuilder builder)
            {
                // Read connection string from environment variable (replace with your path)
                var connectionString = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_SECRET") + "/projects/your-project-id/secrets/app-config/versions/latest";

                // Function to retrieve secrets and populate configuration
                Action<IConfigurationBuilder> populateConfig = (config) =>
                {
                    var secretManager = SecretManagerServiceClient.Create();
                    var accessSecretVersionRequest = new AccessSecretVersionRequest { Name = connectionString };
                    var secretVersion = secretManager.AccessSecretVersion(accessSecretVersionRequest);
                    var payloadBytes = secretVersion.Payload.ToByteArray();
                    //var payload = Encoding.UTF8.GetString(payloadBytes);
                    //var payload = secretVersion.Payload.ToStringUtf8();
                    var payload = new MemoryStream(payloadBytes);

                    // Optionally parse JSON payload
                    if (payloadBytes.Any())
                    {
                        config.AddJsonStream(payload);
                    }

                    // Add environment variables with specified prefixes
                    foreach (var prefix in configPrefixes)
                    {
                        config.AddEnvironmentVariables(prefix: prefix);
                    }
                };

                // Use delegate to avoid capturing variables by value
                // builder.AddConfiguration(populateConfig);
            });
        }

    }
}
