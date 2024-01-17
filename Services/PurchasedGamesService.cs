using System.Linq;
using System.Collections.Generic;

using game_store_api.Entities;
using game_store_api.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace game_store_api.Services
{   
    public class PurchasedGamesService
    {
        private readonly IPurchasedGamesDao _purchasedDao;
        private readonly IGameDao _gameDao;

        public PurchasedGamesService(IPurchasedGamesDao purchasedDao, IGameDao gameDao)
        {
            _purchasedDao = purchasedDao;
            _gameDao = gameDao;
        }

        public List<Game> GetById(int userId)
        {
            List<int> gamesId = _purchasedDao.GetPurchasedGamesId(userId);

            List<Game> purchasedGames = _gameDao.GetAllGames().Where(g => gamesId.Contains(g.GameId)).ToList();

            return purchasedGames;
        }
    }
}
