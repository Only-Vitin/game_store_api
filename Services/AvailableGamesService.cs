using System.Linq;
using System.Collections.Generic;

using game_store_api.Entities;
using game_store_api.Interfaces;
using game_store_api.Data;

namespace game_store_api.Service
{   
    public class AvailableGamesService : IAvailableGamesService
    {
        private readonly Context _context;

        public AvailableGamesService(Context context)
        {
            _context = context;
        }

        public List<Game> SelectAvailableGames(int userId)
        {
            List<int> gamesId = _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
            
            List<Game> selectedGames =
            (from game in _context.Game
            where !gamesId.Contains(game.GameId)
            select game).ToList();

            return selectedGames;
        }
    }
}
