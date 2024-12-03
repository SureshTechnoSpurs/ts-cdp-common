using Newtonsoft.Json;
using System;
using TS.Common.PubSub.Messaging;

namespace TS.Common.Models.PubSub
{
    public class AuthorizationTokenManagerMessage : BaseMessage
    {
        [JsonProperty("RunDate")]
        public DateTime RunDate { get; set; }
    }
}
