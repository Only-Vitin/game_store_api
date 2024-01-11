using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface ITokenStorage
    {
        void AddTokenOnDb(Token tokenClass);
    }
}