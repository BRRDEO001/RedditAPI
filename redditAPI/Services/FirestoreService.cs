using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Grpc.Auth;

public class FirestoreService
{
    private readonly FirestoreDb _db;

    public FirestoreService(string projectId, string credentialsPath)
    {
        GoogleCredential credential = GoogleCredential.FromFile(credentialsPath);
        FirestoreClient firestoreClient = FirestoreClient.Create(projectId, credential.ToChannelCredentials());
        _db = FirestoreDb.Create(projectId, firestoreClient);
    }

    public async Task AddDocument<T>(string collectionName, T document)
    {
        CollectionReference collection = _db.Collection(collectionName);
        await collection.AddAsync(document);
    }
}