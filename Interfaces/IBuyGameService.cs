using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IBuyGameService
    {
        bool VerifyOver18(bool over18, int age);
        void AddPurchaseOnDb(int userId, int gameId);
        string ValidProvidedId(User selectedUser, Game selectedGame);
        string ValidUserForPurchase(User selectedUser, Game selectedGame);
    }
}
