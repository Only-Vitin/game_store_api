using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Repository
{
    public class TokenDaoEfCore : ITokenDao
    {
        private readonly AppDbContext _context;

        public TokenDaoEfCore(AppDbContext context)
        {
            _context = context;
        }

        public void AddTokenOnDb(Token tokenClass)
        {
            _context.Token.Add(tokenClass);
        }
        
        public Token GetTokenByValue(string value)
        {
            return _context.Token.Where(t => t.TokenValue == value).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
