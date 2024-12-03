using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.Common.Models.PubSub;
using TS.Common.PubSub.Messaging;

namespace TS.Common.PubSub.Publisher
{
    public interface IPublisher
    {
        Task PublishAsync(IMessage message, PubSubInput input, bool isBatchMsg = false);
    }
}
