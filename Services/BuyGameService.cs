using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class BuyGameService
    {
        private readonly IPurchasedGamesStorage _purchasedGamesStorage;
 
        public BuyGameService(IPurchasedGamesStorage purchasedGamesStorage)
        {
            _purchasedGamesStorage = purchasedGamesStorage;
        }

        public void Add(int userId, int gameId)
        {
            PurchasedGames newPurchase = new()
            {
                UserId = userId,
                GameId = gameId
            };

            _purchasedGamesStorage.AddNewPurchase(newPurchase);
        }

        public bool VerifyOver18(Game game, User user)
        {
            if(game.Over18)
            {
                if(user.Age < 18) return false;
            }
            return true;
        }

        public bool VerifyBalance(Game game, User user)
        {
            return user.Balance > game.Price;
        }

        public bool VerifyAlreadyPurchased(Game game, User user)
        {
            return _purchasedGamesStorage.GetPurchasedGamesId(user.UserId).Contains(game.GameId);
        }
    }
}
