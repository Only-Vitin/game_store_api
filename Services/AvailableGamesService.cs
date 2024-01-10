using System.Linq;
using System.Collections.Generic;

using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Services
{
    public class AvailableGamesService
    { 
        public static List<Game> SelectAvailableGames(AppDbContext _context, int userId)
        {
            List<int> selectedGamesId = _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
            
            List<Game> selectedGames =
            (from game in _context.Game
            where !selectedGamesId.Contains(game.GameId)
            select game).ToList();

            return selectedGames;
        }
    }
}
