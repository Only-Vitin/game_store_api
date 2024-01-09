using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IBuyGameService
    {
        public bool VerifyOver18(bool over18, int age);
        public void AddPurchaseOnDb(int userId, int gameId);
        public string ValidProvidedId(User selectedUser, Game selectedGame);
        public string ValidUserForPurchase(User selectedUser, Game selectedGame);
    }
}
