using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TS.Common.PubSub.Extentions
{
    /// <summary>
    /// Extention methods for simple use producer/consumer over DependencyInjection
    /// </summary>
    public static class PubSubExtensions
    {
        private const string MessageBrokerConsumerType = "MessageBrokerConsumer:Type";
        private const string MessageBrokerConsumerOptionName = "MessageBrokerConsumer:Options";
        private const string MessageBrokerProducerType = "MessageBrokerProducer:Type";
        private const string MessageBrokerProducerOptionName = "MessageBrokerProducer:Options";

        public static void AddConsumer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var messageBrokerTypeValue = configuration.GetSection(MessageBrokerConsumerType);
            var messageBrokerType = messageBrokerTypeValue.Get<MessageBrokerTypes>();

            switch (messageBrokerType)
            {
                case MessageBrokerTypes.AzureEventHub:
                    //var consumerAzureEventHubConfiguration = configuration.GetSection(MessageBrokerConsumerOptionName).Get<AzureEventHubConsumerConfiguration>();
                    //serviceCollection.AddSingleton<AzureEventHubConsumerConfiguration>(consumerAzureEventHubConfiguration);
                    //serviceCollection.AddSingleton<IConsumer, AzureEventHubConsumer>();
                    break;
                case MessageBrokerTypes.AzureServiceBus:
                    // TODO: Implement consumer for Azure ServiceBus
                    throw new NotImplementedException($"Consumer not implemented for AzureServiceBus");
                default:
                    throw BuildNotSupportedException(messageBrokerTypeValue);
            }
        }

        public static void AddProducer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var messageBrokerTypeValue = configuration.GetSection(MessageBrokerProducerType);
            var messageBrokerType = messageBrokerTypeValue.Get<MessageBrokerTypes>();

            switch (messageBrokerType)
            {
                case MessageBrokerTypes.AzureEventHub:
                    //var producerAzureEventHubConfiguration = configuration.GetSection(MessageBrokerProducerOptionName).Get<AzureEventHubProducerConfiguration>();
                    //serviceCollection.AddSingleton<AzureEventHubProducerConfiguration>(producerAzureEventHubConfiguration);
                    //serviceCollection.AddSingleton<IProducer, AzureEventHubProducer>();
                    break;
                case MessageBrokerTypes.AzureServiceBus:
                    //var producerAzureServiceBusConfiguration = configuration.GetSection(MessageBrokerProducerOptionName).Get<AzureServiceBusProducerConfiguration>();
                    //serviceCollection.AddSingleton<AzureServiceBusProducerConfiguration>(producerAzureServiceBusConfiguration);
                    //serviceCollection.AddSingleton<IProducer, AzureServiceBusProducer>();
                    break;
                default:
                    throw BuildNotSupportedException(messageBrokerTypeValue);
            }
        }

        private static Exception BuildNotSupportedException(IConfigurationSection externalServiceConfiguration)
        {
            return new NotSupportedException($"Unsupported message broker type: {externalServiceConfiguration.Value}");
        }
    }
}
