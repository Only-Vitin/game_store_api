using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface IBuyGameService
    {
        void Add(int userId, int gameId);
        bool VerifyOver18(Game game, User user);
        bool VerifyBalance(Game game, User user);
    }
}
