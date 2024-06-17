using System;
using Newtonsoft.Json;

namespace TS.Common.PubSub.Utilities
{
    public static class PubSubPackageUtility
    {
        public static string Serialize<T>(this T message) where T : class
        {
            return JsonConvert.SerializeObject(message);
        }

        public static T Deserialize<T>(this string messageValue) where T : class
        {
            return JsonConvert.DeserializeObject<T>(messageValue) ?? throw new Exception("Cannot deserialize object");
        }
    }
}
