using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TS.Common.PubSub.Messaging
{
    public class BaseMessage : IMessage
    {
        [JsonProperty("CorrelationId")]
        public Guid CorrelationId { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("MessageTexts")]
        public IEnumerable<string> MessageTexts { get; set; }
    }
}
