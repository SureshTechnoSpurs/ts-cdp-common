using Google.Cloud.Firestore;

namespace TS.Common.Helper
{
    public class FirestoreHelper
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreHelper(string projectId)
        {
            _firestoreDb = FirestoreDb.Create(projectId);
        }

        public async Task<Dictionary<string, string>> GetConfigurationAsync(string collectionId, string documentId)
        {
            DocumentReference docRef = _firestoreDb.Collection(collectionId).Document(documentId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ToDictionary().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            }

            throw new Exception("Configuration not found");
        }
    }
}
