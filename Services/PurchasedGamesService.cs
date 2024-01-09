using System.Linq;
using System.Collections.Generic;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Service
{
    public class PurchasedGamesService : IPurchasedGamesService
    {
        private readonly Context _context;

        public PurchasedGamesService(Context context)
        {
            _context = context;
        }

        public List<Game> SelectPurchasedGames(int userId)
        {
            List<int> gamesId = _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
            
            List<Game> selectedGames =
            (from game in _context.Game
            where gamesId.Contains(game.GameId)
            select game).ToList();

            return selectedGames;
        }
    }
}
