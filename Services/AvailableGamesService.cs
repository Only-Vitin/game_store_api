using System.Linq;
using System.Collections.Generic;

using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{   
    public class AvailableGamesService
    {
        private readonly IPurchasedGamesStorage _purchasedStorage;
        private readonly IGameStorage _gameStorage;

        public AvailableGamesService(IPurchasedGamesStorage purchasedStorage)
        {
            _purchasedStorage = purchasedStorage;
        }

        public List<Game> GetById(int userId)
        {
            List<int> gamesId = _purchasedStorage.GetPurchasedGamesId(userId);
            
            List<Game> availableGames = 
            (from game in _gameStorage.GetAllGames()
            where !gamesId.Contains(game.GameId)
            select game).ToList();

            return availableGames;
        }
    }
}
