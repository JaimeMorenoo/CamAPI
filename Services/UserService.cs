using Camp.Data;
using Camp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<User> CreateUserAsync(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            _anonUserDB.users.Add(newUser);
            await _anonUserDB.SaveChangesAsync();
            return newUser;
        }
        public User Authenticate(string username, string password)
        {
            var user = _anonUserDB.users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            // Authentication successful
            return user;
        }

        public ActionResult<User> GetUserById(int id)
        {
            // Call the repository method to fetch user information by ID
            return _anonUserDB.users.FirstOrDefault(x => x.UserId == id);
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
