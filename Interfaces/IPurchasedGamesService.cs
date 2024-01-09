using System.Collections.Generic;

using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IPurchasedGamesService
    {
        public List<Game> SelectPurchasedGames(int userId);
    }
}
