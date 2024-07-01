using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TS.Common.Authentication.Config
{
    public class GcpConfiguration : IGcpConfiguration
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("private_key_id")]
        public string PrivateKeyId { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }

        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }

        [JsonProperty("auth_provider_x509_cert_url")]
        public string AuthProviderX509CertUrl { get; set; }

        [JsonProperty("client_x509_cert_url")]
        public string ClientX509CertUrl { get; set; }

        //"type": "service_account",
        //"project_id": "customerxi",
        //"private_key_id": "8827adf774de5f5c963008f93ae411923b20e0a5",
        //"private_key": "-----BEGIN PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END PRIVATE KEY-----\n",
        //"client_email": "cxi-service-account@customerxi.iam.gserviceaccount.com",
        //"client_id": "104491185396668982740",
        //"auth_uri": "https://accounts.google.com/o/oauth2/auth",
        //"token_uri": "https://oauth2.googleapis.com/token",
        //"auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
        //"client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/your-service-account-email@your-project-id.iam.gserviceaccount.com"

        public string ToJson(GcpConfiguration config)
        {
            return JsonConvert.SerializeObject(config, Formatting.Indented);
        }

        public GcpConfiguration GetDummyConfiguration()
        {
            return new GcpConfiguration
            {
                Type = "service_account",
                ProjectId = "customerxi",
                PrivateKeyId = "8827adf774de5f5c963008f93ae411923b20e0a5",
                PrivateKey = "-----BEGIN PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END PRIVATE KEY-----\n", // Replace with actual private key (avoid storing in code)
                ClientEmail = "cxi-service-account@customerxi.iam.gserviceaccount.com",
                ClientId = "104491185396668982740",
                AuthUri = "https://accounts.google.com/o/oauth2/auth",
                TokenUri = "https://oauth2.googleapis.com/token",
                AuthProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs",
                ClientX509CertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/your-service-account-email@your-project-id.iam.gserviceaccount.com"
            };
        }
    }
}
