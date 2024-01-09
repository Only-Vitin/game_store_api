using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface ILoginService
    {
        public bool VerifyEmailOnDb(LoginDto login);
        public bool VerifyPassword(LoginDto login, User user);
        public string CreateToken(User user);
        public User SaveTokenOnDb(User user, string token);
    }
}
