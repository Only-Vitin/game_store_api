using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Services
{
    public class BuyGameService
    {
        private readonly IPurchasedGamesDao _purchasedGamesDao;
 
        public BuyGameService(IPurchasedGamesDao purchasedGamesDao)
        {
            _purchasedGamesDao = purchasedGamesDao;
        }

        public void Add(int userId, int gameId)
        {
            PurchasedGames newPurchase = new()
            {
                UserId = userId,
                GameId = gameId
            };

            _purchasedGamesDao.AddNewPurchase(newPurchase);
            _purchasedGamesDao.SaveChanges();
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
            return _purchasedGamesDao.GetPurchasedGamesId(user.UserId).Contains(game.GameId);
        }
    }
}
