using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface ILoginService
    {
        bool VerifyPassword(LoginDto login, User user);
        string CreateToken(User user);
        GetUserDto SaveTokenOnDb(User user, string token);
    }
}
