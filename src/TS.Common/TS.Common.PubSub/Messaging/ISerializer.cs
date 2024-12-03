using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public interface ISerializer
    {
        ValueTask<byte[]> SerializeAsync<T>(T state);

        ValueTask<T> DeserializeAsync<T>(byte[] data);

        object Deserialize(ReadOnlyMemory<byte> data, Type type);
    }
}
