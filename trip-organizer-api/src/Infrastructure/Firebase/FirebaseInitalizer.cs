using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using trip_organizer_api.src.Infrastructure.Config;

namespace trip_organizer_api.src.Infrastructure.Firebase
{
    public static class FirebaseInitializer
    {
        private static bool _initialized = false;
        private static readonly Lock _lock = new();

        public static void Initialize(FirebaseOptions options)
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized) return;
                var appOptions = new AppOptions()
                {
                    ProjectId = options.ProjectId,
                    Credential = GoogleCredential.FromFile(options.CredentialPath),
                };
                FirebaseApp.Create(appOptions);
                _initialized = true;
                Console.WriteLine("Firebase initialized.");
            }
        }

        public static FirestoreDb GetFirestore()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Firebase has not been initialized.");
            }
            return FirestoreDb.Create(FirebaseApp.DefaultInstance.Options.ProjectId);
        }
    }

}