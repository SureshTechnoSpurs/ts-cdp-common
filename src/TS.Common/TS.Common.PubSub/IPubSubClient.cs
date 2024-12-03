using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Common.PubSub
{
    public interface IPubSubClient
    {
        Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts);
        Task<int> PublishBatchMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts);
    }
}
