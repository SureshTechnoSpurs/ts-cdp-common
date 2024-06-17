using TS.Common.PubSub.Consumers;
using TS.Common.PubSubs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace TS.Common.PubSub.Tests
{
    public class AzureEventHubConsumerTest
    {
        private ILogger<MessageBrokerBase> logger;

        public AzureEventHubConsumerTest()
        {
            var loggerMock = new Mock<ILogger<MessageBrokerBase>>();
            logger = loggerMock.Object;
        }

        [Fact]
        public void Initialization_ClientSecretCredential_TenantId_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration();
            Action act = () => new AzureEventHubConsumer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://docs.microsoft.com/partner-center/find-ids-and-domain-names (Parameter 'tenantId')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_ClientId_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
            };

            Action act = () => new AzureEventHubConsumer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'clientId')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_ClientSecret_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
            };

            Action act = () => new AzureEventHubConsumer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'clientSecret')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_BlobStorageUrl_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                
            };

            Action act = () => new AzureEventHubConsumer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'BlobStorageUrl cannot be null')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_ConsumerGroup_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                BlobStorageUrl = "https://contoso",
            };

            var consumer = new AzureEventHubConsumer(configuration, logger);

            Action act = () => consumer.GetMessages((message) => { }).Wait();
            var exception = Assert.Throws<AggregateException>(act);
            Assert.Equal("One or more errors occurred. (Value cannot be null. (Parameter 'consumerGroup'))", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_FullyQualifiedNamespace_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                BlobStorageUrl = "https://contoso",
                ConsumerGroup = "$Default",
            };

            var consumer = new AzureEventHubConsumer(configuration, logger);

            Action act = () => consumer.GetMessages((message) => { }).Wait();
            var exception = Assert.Throws<AggregateException>(act);
            Assert.Equal("One or more errors occurred. (The value '' is not a well-formed Event Hubs fully qualified namespace. (Parameter 'fullyQualifiedNamespace'))", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_EventHubName_Failed()
        {
            var configuration = new AzureEventHubConsumerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                BlobStorageUrl = "https://contoso",
                ConsumerGroup = "$Default",
                FullyQualifiedNamespace = "contoseo.servicebus.windows.net",
            };

            var consumer = new AzureEventHubConsumer(configuration, logger);

            Action act = () => consumer.GetMessages((message) => { }).Wait();
            var exception = Assert.Throws<AggregateException>(act);
            Assert.Equal("One or more errors occurred. (Value cannot be null. (Parameter 'eventHubName'))", exception.Message);
        }
    }
}
