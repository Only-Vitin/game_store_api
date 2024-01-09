using System.Collections.Generic;

using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IPurchasedGamesService
    {
        List<Game> SelectPurchasedGames(int userId);
    }
}
