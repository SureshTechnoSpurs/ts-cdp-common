using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Common.PubSub.Messaging
{
    public class SystemInfo
    {
        public SystemInfo(Guid clientId, string clientGroup)
        {
            if (string.IsNullOrWhiteSpace(clientGroup))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(clientGroup));
            }

            this.ClientId = clientId;
            this.ClientGroup = clientGroup;
        }

        public Guid ClientId { get; }

        public string ClientGroup { get; }

        public bool PublishOnly { get; internal set; }

        public static SystemInfo New()
        {
            SystemInfo systemInfo = new SystemInfo(Guid.NewGuid(), System.AppDomain.CurrentDomain.FriendlyName);

            return systemInfo;
        }
    }
}
