using System.Collections.Generic;

using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IAvailableGamesService
    {
        public List<Game> SelectAvailableGames(int userId);
    }
}
