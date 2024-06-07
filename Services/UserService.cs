using Camp.Data;
using Camp.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> UpdateUserAsync(User model)
        {
            // Search the user
            var user = await _anonUserDB.users.FirstOrDefaultAsync(u => u.UserId == model.UserId);

            
            if (user != null)
            {
                // We change the new values we want
                user.Username = model.Username;
                user.Email = model.Email;
                user.Password = model.Password;
                user.DOB = model.DOB;
                user.LastName = model.LastName;
                user.Name = model.Name;

                await _anonUserDB.SaveChangesAsync();

                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
