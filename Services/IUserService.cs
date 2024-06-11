using Camp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Camp.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        Task<bool> UpdateUserAsync(User model);

        ActionResult<User> GetUserById(int id);

        public Task<User> CreateUserAsync(User newUser);
    }
}
