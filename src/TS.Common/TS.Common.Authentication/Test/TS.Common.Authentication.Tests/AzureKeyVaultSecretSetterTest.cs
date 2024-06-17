using Azure.Authentication.KeyVault.Secrets;
using TS.Common.Authentication.Secrets;
using Moq;
using System;
using Xunit;

namespace TS.Common.Authentication.Tests
{
    public class AzureKeyVaultSecretSetterTest
    {
        private const string SecretName = "secretName1";
        private const string SecretValue = "secretValue1";

        private readonly Mock<ISecretClient> _secretClientMock;
        private readonly AzureKeyVaultSecretSetter _secretSetter;

        public AzureKeyVaultSecretSetterTest()
        {
            _secretClientMock = new Mock<ISecretClient>();
            _secretSetter = new AzureKeyVaultSecretSetter(_secretClientMock.Object);
        }

        [Fact]
        public void Set_CorrectParametersPassed_SuccessfullSecretNameAndValue()
        {
            string secretName = null;
            string secretValue = null;

            _secretClientMock
                .Setup(secretClient => secretClient.SetSecret(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((name, value) =>
                {
                    secretName = name;
                    secretValue = value;
                    return new KeyVaultSecret(name, value); 
                });

            _secretSetter.Set(SecretName, SecretValue, null);

            Assert.Equal(secretName, SecretName);
            Assert.Equal(secretValue, SecretValue);
        }

        [Fact]
        public void Set_CorrectParametersPassed_SuccessfullExpirationData()
        {
            DateTimeOffset? expirationData = null;

            _secretClientMock
                .Setup(secretClient => secretClient.SetSecret(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((name, value) => new KeyVaultSecret(name, value));
            _secretClientMock
                .Setup(secretClient => secretClient.UpdateSecretProperties(It.IsAny<SecretProperties>()))
                .Callback<SecretProperties>(secretProperties => expirationData = secretProperties.ExpiresOn);

            DateTimeOffset? expectedExpirationData = DateTimeOffset.UtcNow;
            _secretSetter.Set(SecretName, SecretValue, expectedExpirationData);

            Assert.Equal(expirationData, expectedExpirationData);
        }
    }
}
