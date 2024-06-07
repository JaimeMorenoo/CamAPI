using Camp.Models;

namespace Camp.Services
{
    public interface IUserService
    {
        bool Authenticate(string username, string password);

        Task<bool> UpdateUserAsync(User model);
    }
}
