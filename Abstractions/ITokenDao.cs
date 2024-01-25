using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface ITokenDao
    {
        void AddTokenOnDb(Token tokenClass);
        Token GetTokenByValue(string value);
        void SaveChanges();
    }
}
