namespace redditAPI.Services;

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using redditAPI.DataModels;

public class FirestoreService
{
    private readonly FirestoreDb db;
    string pathToServiceAccountKey = "./fbServiceAccountKey.json";

    public FirestoreService(string projectId, string credentialsPath)
    {
        GoogleCredential credential = GoogleCredential.FromFile(credentialsPath);
        FirestoreClient firestoreClient = Google.Cloud.Firestore.V1.FirestoreClient.Create();
        db = FirestoreDb.Create(projectId, firestoreClient);
    }

    public async Task AddDocument<T>(string collectionName, T document)
    {
        CollectionReference collection = db.Collection(collectionName);
        await collection.AddAsync(document);
    }

    public async Task UpdateDocument(string collectionName, string documentId, Post updatedPost)
    {
        var documentReference = db.Collection(collectionName).Document(documentId);
        await documentReference.SetAsync(updatedPost, SetOptions.MergeAll);
    }

    public async Task DeleteDocument(string collectionName, string documentId)
    {
        var documentReference = db.Collection(collectionName).Document(documentId);
        await documentReference.DeleteAsync();
    }

    public async Task<List<Post>> GetCollection(string collectionName)
    {
        CollectionReference collection = db.Collection(collectionName);
        QuerySnapshot snapshot = await collection.GetSnapshotAsync();

        List<Post> posts = new List<Post>();
        foreach(DocumentSnapshot document in snapshot.Documents)
        {
            if (document.Exists)
            {
                Post post = document.ConvertTo<Post>();
                posts.Add(post);
            }
        }

        return posts;
    }

    public async Task<Post> GetDocument(string collectionName, string documentId)
    {
        DocumentReference documentReference = db.Collection(collectionName).Document(documentId);
        DocumentSnapshot snapshot = await documentReference.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Post post = snapshot.ConvertTo<Post>();
            return post;
        }

        return null;
    }

    public async Task LikePost(string id)
    {
        DocumentReference postsRef = db.Collection("posts").Document(id);
        DocumentSnapshot snapshot = await postsRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Post post = snapshot.ConvertTo<Post>();
            post.likeCount++;
            await postsRef.SetAsync(post);
        }
        else
        {
            throw new Exception("Post not found");
        }

    }

    public async Task UnlikePost(string id)
    {
        DocumentReference postsRef = db.Collection("posts").Document(id);
        DocumentSnapshot snapshot = await postsRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Post post = snapshot.ConvertTo<Post>();
            post.likeCount--;
            await postsRef.SetAsync(post);
        }
        else
        {
            throw new Exception("Post not found");
        }

    }


}