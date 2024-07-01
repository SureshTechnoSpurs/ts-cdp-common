using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;
using TS.Common.Constants;

namespace TS.Common.Authentication.Helper
{
    public class ServiceAccountCredentialsHelper
    {
        private readonly ServiceAccountCredential _serviceAccountCredential;
        private readonly string _ServiceAccountAudience;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly IGcpConfiguration _gcpConfiguration;

        public ServiceAccountCredentialsHelper(IAuthenticationClient authenticationClient, IGcpConfiguration gcpConfiguration)
        {
            _authenticationClient = authenticationClient;
            _gcpConfiguration = gcpConfiguration;
            //_serviceAccountCredential = ServiceAccountCredential.FromServiceAccountData(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
            _ServiceAccountAudience = "https://www.googleapis.com/oauth2/v4/token";
        }

        public async Task<string> GenerateJwtToken()
        {
            var now = DateTime.UtcNow;
            var payload = new[]
            {
            new Claim(JwtRegisteredClaimNames.Iss, _serviceAccountCredential.Id),
            new Claim(JwtRegisteredClaimNames.Sub, _serviceAccountCredential.Id),
            new Claim(JwtRegisteredClaimNames.Aud, _ServiceAccountAudience),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };

            var googleCredentials = await _authenticationClient.GetCredentialAsync();

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
