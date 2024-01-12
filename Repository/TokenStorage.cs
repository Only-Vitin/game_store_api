using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Repository.Storage
{
    public class TokenStorage : ITokenStorage
    {
        private readonly AppDbContext _context;

        public TokenStorage(AppDbContext context)
        {
            _context = context;
        }

        public void AddTokenOnDb(Token tokenClass)
        {
            _context.Token.Add(tokenClass);
            _context.SaveChanges();
        }
        
        public Token GetTokenByValue(string value)
        {
            return _context.Token.Where(t => t.TokenValue == value).SingleOrDefault();
        }
    }
}
