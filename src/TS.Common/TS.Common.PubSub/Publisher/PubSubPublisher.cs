using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Grpc.Auth;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TS.Common.Authentication;
using TS.Common.Models.PubSub;
using TS.Common.PubSub.Messaging;
using TS.Common.PubSub.Utilities;

namespace TS.Common.PubSub.Publisher
{
    public class PubSubPublisher : IPublisher
    {
        private readonly IAuthenticationClient _authenticationClient;
        public PubSubPublisher(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        public Task PublishAsync(IMessage message, PubSubInput input, bool isBatchMsg = false)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (isBatchMsg == true)
            {
                return this.PublishBatchMessagesAsync(input, message);
            }
            else
            {
                return this.PublishMessagesAsync(input, message);
            }
        }


        public async Task<int> PublishMessagesAsync(PubSubInput input, IMessage message)
        {
            string projectId = input.ProjectId;
            string topicId = input.TopicId;
            var serializer = new JsonSerializer();
            var messageTexts = await serializer.SerializeAsync(message);

            // Load GoogleCredentials from a service account key file
            GoogleCredential googleCredential = await _authenticationClient.GetCredentialAsync();

            // Create a TopicName instance
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);

            // Create a PublisherClientBuilder with the credentials
            PublisherClientBuilder builder = new PublisherClientBuilder
            {
                ChannelCredentials = googleCredential.ToChannelCredentials(),
                TopicName = topicName
            };

            // Create the PublisherClient using the builder
            PublisherClient publisher = await builder.BuildAsync();

            int publishedMessageCount = 0;
            var publishTasks = messageTexts.Select(async text =>
            {
                try
                {
                    // Create a PubsubMessage instance
                    PubsubMessage pubsubMessage = new PubsubMessage
                    {
                        MessageId = message.Id.ToString(),
                        Data = Google.Protobuf.ByteString.CopyFromUtf8(Convert.ToBase64String(messageTexts))
                    };

                    // Publish the message to the specified topic
                    string messageId = await publisher.PublishAsync(pubsubMessage);
                    Console.WriteLine($"Published message {messageId}");
                    Interlocked.Increment(ref publishedMessageCount);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"An error occurred when publishing message {text}: {exception.Message}");
                }
            });

            await Task.WhenAll(publishTasks);
            return publishedMessageCount;
        }

        public async Task<int> PublishBatchMessagesAsync(PubSubInput input, IMessage message)
        {
            string projectId = input.ProjectId;
            string topicId = input.TopicId;
            var messageTexts = message.MessageTexts;

            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);

            // Default Settings:
            // byteCountThreshold: 1000000
            // elementCountThreshold: 100
            // delayThreshold: 10 milliseconds
            var customSettings = new PublisherClient.Settings
            {
                BatchingSettings = new BatchingSettings(
                    elementCountThreshold: input.ElementCountThreshold,
                    byteCountThreshold: input.ByteCountThreshold,
                    delayThreshold: TimeSpan.FromMilliseconds(input.DelayThreshold))
            };

            PublisherClient publisher = await new PublisherClientBuilder
            {
                TopicName = topicName,
                Settings = customSettings
            }.BuildAsync();

            int publishedMessageCount = 0;
            var publishTasks = messageTexts.Select(async text =>
            {
                try
                {
                    string message = await publisher.PublishAsync(text);
                    Console.WriteLine($"Published message {message}");
                    Interlocked.Increment(ref publishedMessageCount);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"An error occurred when publishing message {text}: {exception.Message}");
                }
            });
            await Task.WhenAll(publishTasks);
            return publishedMessageCount;
        }
    }
}
