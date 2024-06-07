using Camp.Data;

namespace Camp.Services
{
    public class UserService : IUserService
    {
        private readonly AnonUserDB _anonUserDB; 
        public UserService(AnonUserDB anonUserDB)
        {
            _anonUserDB = anonUserDB;
        }
        // Here is where we are checking with the database if the credentials given are correct.
        public bool Authenticate(string username, string password)
        {
            return _anonUserDB.users.Any(x=> x.Username == username && x.Password == password);
        }
    }
}
