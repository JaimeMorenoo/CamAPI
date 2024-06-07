namespace Camp.Services
{
    public interface IUserService
    {
        bool Authenticate(string username, string password);
    }
}
