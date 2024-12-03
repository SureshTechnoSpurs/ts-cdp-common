using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public class MessageContext<TM> : IMessageContext<TM>
        where TM : IMessage
    {
        public MessageContext(TM message, SystemInfo systemInfo)
        {
            this.SystemInfo = systemInfo ?? throw new ArgumentNullException(nameof(systemInfo));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public TM Message { get; }

        public SystemInfo SystemInfo { get; }
    }
}
