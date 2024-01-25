using System.Collections.Generic;
using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Repository
{
    public class PurchasedGamesDaoEfCore : IPurchasedGamesDao
    {
        private readonly AppDbContext _context;

        public PurchasedGamesDaoEfCore(AppDbContext context)
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
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
