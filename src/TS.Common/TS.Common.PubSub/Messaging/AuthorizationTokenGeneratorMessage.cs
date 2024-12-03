using Newtonsoft.Json;
using System;

namespace TS.Common.PubSub.Messaging
{
    public class AuthorizationTokenGeneratorMessage : BaseMessage
    {
        [JsonProperty("RunDate")]
        public DateTime RunDate { get; set; }

        [JsonProperty("POSType")]
        public string POSType { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }
    }
}
