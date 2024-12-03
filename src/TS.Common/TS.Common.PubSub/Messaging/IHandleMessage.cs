using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public interface IHandleMessage<in TM>
       where TM : IMessage
    {
        Task HandleAsync(IMessageContext<TM> context);
    }
}
