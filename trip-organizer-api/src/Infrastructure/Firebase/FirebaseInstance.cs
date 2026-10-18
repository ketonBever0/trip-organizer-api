using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using trip_organizer_api.src.Infrastructure.Config;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace trip_organizer_api.src.Infrastructure.Firebase
{
    public static class FirebaseInstance
    {
        private static bool _initialized = false;
        private static FirestoreDb? _db;
        private static FirebaseAuthClient? _auth;
        private static readonly Lock _lock = new();

        public static void Initialize(FirebaseOptions options, FirebaseClientConfig clientOptions)
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized) return;

                var appOptions = new AppOptions()
                {
                    ProjectId = options.ProjectId,
                    Credential = GoogleCredential.FromFile(options.CredentialPath)
                };
                FirebaseApp.Create(appOptions);
                //_db = FirestoreDb.Create(options.ProjectId);
                _db = new FirestoreDbBuilder { ProjectId = options.ProjectId, Credential = appOptions.Credential }.Build();
                _auth = new FirebaseAuthClient(new FirebaseAuthConfig
                {
                    ApiKey = clientOptions.ApiKey,
                    AuthDomain = clientOptions.AuthDomain,
                    Providers = [
                        new EmailProvider()
                    ]
                });
                _initialized = true;
                Console.WriteLine("Firebase initialized.");
                //Console.WriteLine("FirebaseApp: " + (FirebaseApp.DefaultInstance != null));
                //Console.WriteLine("FirebaseAuth: " + (FirebaseAuth.DefaultInstance != null));
            }
        }

        public static FirebaseAuth GetAuth()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Firebase has not been initialized.");
            }
            return FirebaseAuth.DefaultInstance;
        }

        public static FirebaseAuthClient GetAuthClient()
        {
            if (!_initialized || _auth == null)
            {
                throw new InvalidOperationException("Firebase has not been initialized.");
            }
            return _auth;
        }

        public static FirestoreDb GetFirestore()
        {
            if (!_initialized || _db == null)
            {
                throw new InvalidOperationException("Firebase has not been initialized.");
            }
            return _db;
        }
    }

}