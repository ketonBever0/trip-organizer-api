using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using trip_organizer_api.src.Infrastructure.Config;

namespace trip_organizer_api.src.Infrastructure.Firebase
{
    public static class FirebaseInitializer
    {
        private static bool _initialized = false;

        public static void Initialize(FirebaseOptions options)
        {
            if (_initialized) return;

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(options.CredentialPath),
                ProjectId = options.ProjectId
            });

            _initialized = true;
        }

        public static FirestoreDb GetFirestore()
        {
            return FirestoreDb.Create(FirebaseApp.DefaultInstance.Options.ProjectId);
        }
    }

}