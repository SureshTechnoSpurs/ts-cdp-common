using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public interface IMessage
    {
        Guid Id { get; }

        Guid CorrelationId { get; }

        IEnumerable<string> MessageTexts { get; }
    }
}
