using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public ValueTask<byte[]> SerializeAsync<T>(T state)
        {
            string json = JsonConvert.SerializeObject(state, Settings);
            return ValueTask.FromResult(Encoding.UTF8.GetBytes(json));
        }

        public ValueTask<T> DeserializeAsync<T>(byte[] data)
        {
            string json = Encoding.UTF8.GetString(data);
            return ValueTask.FromResult(JsonConvert.DeserializeObject<T>(json, Settings));
        }

        public object Deserialize(ReadOnlyMemory<byte> data, Type type)
        {
            string json = Encoding.UTF8.GetString(data.Span);
            return JsonConvert.DeserializeObject(json, type, Settings);
        }
    }
}
