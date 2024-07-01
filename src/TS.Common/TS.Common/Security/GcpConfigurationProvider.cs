using TS.Common.Helper;

namespace TS.Common.Security
{
    public class GcpConfigurationProvider : ConfigurationProvider
    {
        private readonly string _secretVersion;
        private readonly FirestoreHelper _firestoreHelper;
        private readonly string _firestoreCollection;
        private readonly string _firestoreDocument;

        public GcpConfigurationProvider(string secretVersion, FirestoreHelper firestoreHelper, string firestoreCollection, string firestoreDocument)
        {
            _secretVersion = secretVersion;
            _firestoreHelper = firestoreHelper;
            _firestoreCollection = firestoreCollection;
            _firestoreDocument = firestoreDocument;
        }

        public override void Load()
        {
            var configData = new Dictionary<string, string>();

            // Load secrets from Secret Manager
            var secretData = SecretManagerHelper.GetSecret(_secretVersion);
            configData.Add("SecretConfig", secretData);

            // Load configuration from Firestore
            var firestoreTask = _firestoreHelper.GetConfigurationAsync(_firestoreCollection, _firestoreDocument);
            firestoreTask.Wait();
            foreach (var kvp in firestoreTask.Result)
            {
                configData[kvp.Key] = kvp.Value;
            }

            Data = configData;
        }
    }
}
