using System.Linq;
using System.Collections.Generic;

using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Services
{   
    public class AvailableGamesService
    {
        private readonly IPurchasedGamesDao _purchasedDao;
        private readonly IGameDao _gameDao;

        public AvailableGamesService(IPurchasedGamesDao purchasedDao, IGameDao gameDao)
        {
            _purchasedDao = purchasedDao;
            _gameDao = gameDao;
        }

        public List<Game> GetById(int userId)
        {
            List<int> gamesId = _purchasedDao.GetPurchasedGamesId(userId);

            List<Game> availableGames = _gameDao.GetAllGames().Where(g => !gamesId.Contains(g.GameId)).ToList();

            return availableGames;
        }
    }
}
