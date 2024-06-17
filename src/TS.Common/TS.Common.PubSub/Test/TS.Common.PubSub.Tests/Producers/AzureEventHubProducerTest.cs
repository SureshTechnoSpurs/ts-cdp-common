using TS.Common.PubSub.Producers;
using TS.Common.PubSubs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace TS.Common.PubSub.Tests.Producers
{
    public class AzureEventHubProducerTest
    {
        private ILogger<MessageBrokerBase> logger;

        public AzureEventHubProducerTest()
        {
            var loggerMock = new Mock<ILogger<MessageBrokerBase>>();
            logger = loggerMock.Object;
        }

        [Fact]
        public void Initialization_ClientSecretCredential_TenantId_Failed()
        {
            var configuration = new AzureServiceBusProducerConfiguration();
            Action act = () => new AzureServiceBusProducer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Tenant id cannot be null. You can locate your tenant id by following the instructions listed here: https://docs.microsoft.com/partner-center/find-ids-and-domain-names (Parameter 'tenantId')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_ClientId_Failed()
        {
            var configuration = new AzureServiceBusProducerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
            };

            Action act = () => new AzureServiceBusProducer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'clientId')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_ClientSecret_Failed()
        {
            var configuration = new AzureServiceBusProducerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
            };

            Action act = () => new AzureServiceBusProducer(configuration, logger);
            ArgumentException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("Value cannot be null. (Parameter 'clientSecret')", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_FullyQualifiedNamespace_Failed()
        {
            var configuration = new AzureServiceBusProducerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
            };

            var producer = new AzureServiceBusProducer(configuration, logger);

            Action act = () => producer.SendMessages(new string[] { }).Wait();
            var exception = Assert.Throws<AggregateException>(act);
            Assert.Equal("One or more errors occurred. (The value '' is not a well-formed Service Bus fully qualified namespace. (Parameter 'fullyQualifiedNamespace'))", exception.Message);
        }

        [Fact]
        public void Initialization_ClientSecretCredential_EntityPath_Failed()
        {
            var configuration = new AzureServiceBusProducerConfiguration()
            {
                TenantId = Guid.NewGuid().ToString(),
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                FullyQualifiedNamespace = "contoseo.servicebus.windows.net",
            };

            var producer = new AzureServiceBusProducer(configuration, logger);

            Action act = () => producer.SendMessages(new string[] { }).Wait();
            var exception = Assert.Throws<AggregateException>(act);
            Assert.Equal("One or more errors occurred. (Value cannot be null. (Parameter 'entityPath'))", exception.Message);
        }
    }
}
