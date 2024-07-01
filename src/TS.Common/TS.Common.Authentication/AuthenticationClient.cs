using Google.Apis.Auth.OAuth2;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;

namespace TS.Common.Authentication
{
    public class AuthenticationClient : IAuthenticationClient
    {
        public AuthenticationClient()
        {
        }

        private readonly IGcpConfiguration _gcpConfiguration;
        public AuthenticationClient(IGcpConfiguration configuration)
        {
            _gcpConfiguration = configuration;
        }
        public async Task<GoogleCredential> GetCredentialAsync()
        {
            string inputJson = _gcpConfiguration.ToJson();
            // Load GoogleCredentials from a service account key file
            var googleCredential = GoogleCredential.FromJson(inputJson);

            return googleCredential;

        }
    }
}
