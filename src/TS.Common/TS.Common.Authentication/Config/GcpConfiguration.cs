using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TS.Common.Authentication.Config
{
    public class GcpConfiguration : IGcpConfiguration
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("private_key_id")]
        public string PrivateKeyId { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }

        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }

        [JsonProperty("auth_provider_x509_cert_url")]
        public string AuthProviderX509CertUrl { get; set; }

        [JsonProperty("client_x509_cert_url")]
        public string ClientX509CertUrl { get; set; }

        [JsonProperty("universe_domain")]
        public string Universe_domain { get; set; }
        
        //"type": "service_account",
        //"project_id": "customerxi",
        //"private_key_id": "8827adf774de5f5c963008f93ae411923b20e0a5",
        //"private_key": "-----BEGIN PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END PRIVATE KEY-----\n",
        //"client_email": "cxi-service-account@customerxi.iam.gserviceaccount.com",
        //"client_id": "104491185396668982740",
        //"auth_uri": "https://accounts.google.com/o/oauth2/auth",
        //"token_uri": "https://oauth2.googleapis.com/token",
        //"auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
        //"client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/your-service-account-email@your-project-id.iam.gserviceaccount.com"

        public string ToJson(GcpConfiguration config)
        {
            return JsonConvert.SerializeObject(config, Formatting.Indented);
        }

        public GcpConfiguration GetDummyConfiguration()
        {
            return new GcpConfiguration
            {
                Type = "service_account",
                ProjectId = "customerxi",
                PrivateKeyId = "8827adf774de5f5c963008f93ae411923b20e0a5",
                PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQDIRmZ4pNdbupKL\nBh2QUOWWxHwAAL2onUFxVwIf//FhQWMpizsThpYQdJ1G0QIvyD5uxJ0+8vEFFvla\nvhrf7s9UQ08kgEVtZE0wT54P2EG2YVIp3pjC/2QHBQkQ924td1Ai4/ExSBSrwMp8\njINEiCAXeDH+K2U4m4rxpVpz5rOsQIgULNkYbuiPBJo2n1bl3atrgjSmiGeHMLZb\n42lPO7wrq2ytM+6++x7jE/xQpXiPTEqOJwILEh5ra6AFgxwAIBoPve+yU5bBb96o\nyuKSVw9I2CrCg+/SGqxniomSvo69FRwda612/CLiTRrV3VQMHypun8maHDXK8c0l\n38MKKgu1AgMBAAECgf81OzMisV/EAcfmY2vRPdvIK9V7pSYRzGyCRDnoMTVQKdle\nBOaCVTg/w0SmNOd2JNloKFSYGMLVrb/9XnPGMsmEb4Dni8xMc58x9UVm2ngthyr2\ne4zQ+ZZDlUqyN8PklI8cXejqMF/TtiesbYAHG0i6QiH8bJd62Himc911dmz9Ocd1\nW3RbTq//8FtHmCw8gzKfGDehyeCgjc48pLx0+PnjLBjkDLT+rub0eqf4RhDHGqHd\nNrs459NWHzM7zP4K+/jnb77loCvcXNCUeZgMMUCob53BnctNlw1adMyGUTTOlVAM\nXoO1IUeroDqsFaKDLT9Er4rCrUVrKV4uqhOP82UCgYEA7m/bTkeA8JhEu8TZGbvX\ncKfEmXzIlgghiPjD0LgILE/EzeumVIaGrxsdhxdXBC+JJAjxVIMVYghtCdY6wtg7\nRbVlWWNW+zlaHC7t1Y/Eav52OqZfZYZbdkttZHF0ourg6Ui8NgNnCYleFFFDLib0\nr3BDfCGHpz69oHHvhQZWGZsCgYEA1wbvEYkyV3usC0KvRX0pPOC9OzCY7tk0J03N\n6Iecv9LWKBaNUO8DIx2NRJZ7ofuBUlDKF9opNny15vauY48JQBSYji8AHOQiRGjo\nArOM7qCdbijYNGSjNijCcMZknX2bnt63KfQvEry+VGedFRdGigERHanP0ReUgfEo\nr1b8rO8CgYEAgAC+3DxYxVEJfUBFSKJmG02FptytlpXQ4wahoPhbMua0AdRjW/dI\ncNL+vBpUlaOrXRQL8tHYdeOOHfzLWPTahSbHvQAunvoAHSc4eTEQQlxPPlc42XkS\nuuPtW0HpWtj7W2G34Mtc0mrTfdbOWQWC0HhVUltxxuBQzsS70E9Bg8cCgYB/d4rM\n+GmxVozceAFyzgleH1PN/LcMEBJMomDH0WxoglGVnhjTu3w+mswdtp8/nNpCs8W5\ncggwVql2axFhz28KX6s3zkDg35Q4vX+b6lhHNUtd/DR+ipH+DxeExvbqLYXNHfWy\nlyA4mU6ytmF2GUAlmxBMZ1fceX/9r7oQhx5uXQKBgQCH0VqmUJKWQJq33N96+XIv\nuG2Bp1bjZdFAJUI/LM50MW5ieAKN8mK7RBAHBuFIO0mX7keWmE1jtb3ndQGJU/Cs\nrPtWNT7r8mG9Tcj+JJsQkAmM+7038K2LdxaTYu50Ro5dYv7JRLnCyhHu4BYFvmjW\nt8F3gNkxH2tqIIPJ+2YWfg==\n-----END PRIVATE KEY-----\n", // Replace with actual private key (avoid storing in code)
                ClientEmail = "cxi-service-account@customerxi.iam.gserviceaccount.com",
                ClientId = "104491185396668982740",
                AuthUri = "https://accounts.google.com/o/oauth2/auth",
                TokenUri = "https://oauth2.googleapis.com/token",
                AuthProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs",
                ClientX509CertUrl = "https://www.googleapis.com/robot/v1/metadata/x509/your-service-account-email@your-project-id.iam.gserviceaccount.com",
                Universe_domain = "googleapis.com"
            };
        }
    }
}
