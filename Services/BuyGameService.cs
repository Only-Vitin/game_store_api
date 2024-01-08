using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Service
{
    public class BuyGameService
    {
        public static bool VerifyOver18(bool over18, int age)
        {
            if(over18)
            {
                if(age < 18) return false;
            }
            return true;
        }

        public static void AddPurchaseOnDb(int userId, int gameId, Context _context)
        {
            PurchasedGames newPurchase = new()
            {
                UserId = userId,
                GameId = gameId
            };

            _context.PurchasedGames.Add(newPurchase);
            _context.SaveChanges();
        }
    }
}
