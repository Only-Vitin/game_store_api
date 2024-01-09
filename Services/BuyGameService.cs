using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Service
{
    public class BuyGameService : IBuyGameService
    {
        private readonly Context _context;

        public BuyGameService(Context context)
        {
            _context = context;
        }

        public bool VerifyOver18(bool over18, int age)
        {
            if(over18)
            {
                if(age < 18) return false;
            }
            return true;
        }

        public void AddPurchaseOnDb(int userId, int gameId)
        {
            PurchasedGames newPurchase = new()
            {
                UserId = userId,
                GameId = gameId
            };

            _context.PurchasedGames.Add(newPurchase);
            _context.SaveChanges();
        }

        public string ValidProvidedId(User user, Game game)
        {
            if(user == null) return "Usuário não encontrado";
            if(game == null) return "Jogo não encontrado";

            PurchasedGames purchasedGame = _context.PurchasedGames.Where(pg => pg.GameId == game.GameId).SingleOrDefault();
            if(purchasedGame != null) return "O jogo já foi comprado";
            
            return "Ok";
        }

        public string ValidUserForPurchase(User user, Game game)
        {
            if(!VerifyOver18(game.Over18, user.Age)) return "Necessário ter mais de 18 anos para comprar esse jogo";
            if(user.Balance < game.Price) return "Saldo insuficiente";
            
            return "Ok";
        }
    }
}
