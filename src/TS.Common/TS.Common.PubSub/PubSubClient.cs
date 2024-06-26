using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Common.Authentication;

namespace TS.Common.PubSub
{
    public class PubSubClient : IPubSubClient
    {
        private readonly IAuthenticationClient _authenticationClient;
        public PubSubClient(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }
    }
}
