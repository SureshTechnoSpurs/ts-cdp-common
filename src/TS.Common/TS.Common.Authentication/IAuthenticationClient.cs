using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Common.Authentication.Config;

namespace TS.Common.Authentication
{
    public interface IAuthenticationClient
    {
        Task<GoogleCredential> GetCredentialAsync();
        //string ToJson(GcpConfiguration config);
    }
}
