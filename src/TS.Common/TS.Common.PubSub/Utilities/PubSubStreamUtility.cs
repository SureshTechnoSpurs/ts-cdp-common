using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TS.Common.PubSub.Utilities
{
    public static class PubSubStreamUtility
    {
        public static Stream SerializeToStream<T>(this T obj) where T : class
        {
            using var memStream = new MemoryStream();
            var objString = obj.Serialize();
            return new MemoryStream(Encoding.UTF8.GetBytes(objString));
        }

        public static T DeserializeFromStream<T>(this Stream dataStream)
        {
            var serializer = new JsonSerializer();

            var sr = new StreamReader(dataStream);
            var jsonTextReader = new JsonTextReader(sr);

            return serializer.Deserialize<T>(jsonTextReader);
        }
    }
}