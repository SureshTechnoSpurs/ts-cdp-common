﻿using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;
using TS.Common.Constants;

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
            var config = _gcpConfiguration.GetDummyConfiguration();
            string inputJson = _gcpConfiguration.ToJson(config);

            // Load GoogleCredentials from a service account key file
            var googleCredential = GoogleCredential.FromJson(inputJson);

            return googleCredential;

        }

        public async Task<string> GenerateJwtToken()
        {
            var now = DateTime.UtcNow;
            var payload = new[]
            {
            new Claim(JwtRegisteredClaimNames.Iss, _gcpConfiguration.ClientEmail),
            new Claim(JwtRegisteredClaimNames.Sub, _gcpConfiguration.ClientEmail),
            new Claim(JwtRegisteredClaimNames.Aud, ServiceAccount.Audience),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };

            //var googleCredentials = await GetCredentialAsync();

            using var rsa = RSA.Create();
            rsa.ImportFromPem(_gcpConfiguration.PrivateKey.ToCharArray());

            //var credentials = new SigningCredentials(
            //    new X509SecurityKey(new X509Certificate2(googleCredentials.Key.Export(X509ContentType.Pkcs12))),
            //    SecurityAlgorithms.RsaSha256
            //);

            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                claims: payload,
                signingCredentials: signingCredentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
