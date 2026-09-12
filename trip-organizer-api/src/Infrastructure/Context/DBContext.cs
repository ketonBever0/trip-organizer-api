using trip_organizer_api.src.Application;
using trip_organizer_api.src.Application.Interfaces;
using trip_organizer_api.src.Infrastructure.Firebase.Repositories;

namespace trip_organizer_api.src.Infrastructure.Context
{
    public class DBContext : IDBContext
    {
        public IAuthRepository Auth { get; set; }
        public IUserRepository User { get; set; }

        public DBContext(
            DBSelector selector,
            FirebaseAuthRepository firebaseAuth, FirebaseAuthRepository mysqlAuth,
            FirebaseUserRepository firebaseUser, FirebaseUserRepository mysqlUser
            )
        {
            switch (selector.DBType.ToLower())
            {
                case "firebase":
                    Auth = firebaseAuth;
                    User = firebaseUser;
                    break;
                case "mysql":
                    Auth = mysqlAuth;
                    User = mysqlUser;
                    break;
                default:
                    throw new ArgumentException($"Unsupported DBType: {selector.DBType}");
            }
        }
    }
}
