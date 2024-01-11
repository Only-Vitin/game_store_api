using System.Linq;
using System.Collections.Generic;

using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{   
    public class PurchasedGamesService
    {
        private readonly IPurchasedGamesStorage _purchasedStorage;
        private readonly IGameStorage _gameStorage;

        public PurchasedGamesService(){}
        public PurchasedGamesService(IPurchasedGamesStorage purchasedStorage)
        {
            _purchasedStorage = purchasedStorage;
        }

        public List<Game> GetById(int userId)
        {
            List<int> gamesId = _purchasedStorage.GetPurchasedGamesId(userId);
            
            List<Game> purchasedGames = 
            (from game in _gameStorage.GetAllGames()
            where gamesId.Contains(game.GameId)
            select game).ToList();

            return purchasedGames;
        }
    }
}