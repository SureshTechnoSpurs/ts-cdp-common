using Google.Api.Gax;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Grpc.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.Common.Authentication;

namespace TS.Common.PubSub
{
    public class PubSubClient : IPubSubClient
    {
        private readonly IAuthenticationClient _authenticationClient;
        public PubSubClient(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        //public async Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts)
        //{
        //    TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
        //    PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

        //    int publishedMessageCount = 0;
        //    var publishTasks = messageTexts.Select(async text =>
        //    {
        //        try
        //        {
        //            string message = await publisher.PublishAsync(text);
        //            Console.WriteLine($"Published message {message}");
        //            Interlocked.Increment(ref publishedMessageCount);
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine($"An error occurred when publishing message {text}: {exception.Message}");
        //        }
        //    });
        //    await Task.WhenAll(publishTasks);
        //    return publishedMessageCount;
        //}

        public async Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts)
        {
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
                        Data = Google.Protobuf.ByteString.CopyFromUtf8(text)
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

        public async Task<int> PublishBatchMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts)
        {
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);

            // Default Settings:
            // byteCountThreshold: 1000000
            // elementCountThreshold: 100
            // delayThreshold: 10 milliseconds
            var customSettings = new PublisherClient.Settings
            {
                BatchingSettings = new BatchingSettings(
                    elementCountThreshold: 50,
                    byteCountThreshold: 10240,
                    delayThreshold: TimeSpan.FromMilliseconds(500))
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
