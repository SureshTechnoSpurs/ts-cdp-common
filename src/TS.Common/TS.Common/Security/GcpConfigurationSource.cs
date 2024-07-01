using TS.Common.Helper;

namespace TS.Common.Security
{
    public class GcpConfigurationSource : IConfigurationSource
    {
        private readonly string _secretVersion;
        private readonly FirestoreHelper _firestoreHelper;
        private readonly string _firestoreCollection;
        private readonly string _firestoreDocument;

        public GcpConfigurationSource(string secretVersion, FirestoreHelper firestoreHelper, string firestoreCollection, string firestoreDocument)
        {
            _secretVersion = secretVersion;
            _firestoreHelper = firestoreHelper;
            _firestoreCollection = firestoreCollection;
            _firestoreDocument = firestoreDocument;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new GcpConfigurationProvider(_secretVersion, _firestoreHelper, _firestoreCollection, _firestoreDocument);
        }
    }
}
