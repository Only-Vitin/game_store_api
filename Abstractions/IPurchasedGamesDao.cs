using System.Collections.Generic;

using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface IPurchasedGamesDao
    {
        void AddNewPurchase(PurchasedGames newPurchase);
        List<int> GetPurchasedGamesId(int userId);
    }
}
