using System.Collections.Generic;
using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Repository
{
    public class PurchasedGamesStorage : IPurchasedGamesStorage
    {
        private readonly AppDbContext _context;

        public PurchasedGamesStorage(AppDbContext context)
        {
            _context = context;
        }

        public List<int> GetPurchasedGamesId(int userId)
        {
            return _context.PurchasedGames.Where(pg => pg.UserId == userId).Select(pg => pg.GameId).ToList();
        }

        public void AddNewPurchase(PurchasedGames newPurchase)
        {
            _context.PurchasedGames.Add(newPurchase);
            _context.SaveChanges();
        }
    }
}
