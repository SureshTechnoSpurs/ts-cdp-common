using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public interface IMessageContext<out TM>
        where TM : IMessage
    {
        TM Message { get; }

        SystemInfo SystemInfo { get; }
    }
}
