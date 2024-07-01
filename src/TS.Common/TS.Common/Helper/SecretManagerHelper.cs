using Google.Cloud.SecretManager.V1;

namespace TS.Common.Helper
{
    public class SecretManagerHelper
    {
        public static string GetSecret(string secretVersion)
        {
            var secretManager = SecretManagerServiceClient.Create();
            var secretVersionName = SecretVersionName.Parse(secretVersion);
            var secretVersionResponse = secretManager.AccessSecretVersion(new AccessSecretVersionRequest
            {
                SecretVersionName = secretVersionName
            });

            var payload = secretVersionResponse.Payload.Data;
            if (payload == null)
            {
                throw new Exception("Secret payload is null");
            }

            return payload.ToStringUtf8();
        }
    }
}
