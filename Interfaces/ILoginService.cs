using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface ILoginService
    {
        bool VerifyEmailOnDb(LoginDto login);
        bool VerifyPassword(LoginDto login, User user);
        string CreateToken(User user);
        User SaveTokenOnDb(User user, string token);
    }
}
