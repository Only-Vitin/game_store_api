using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Storage
{
    public class TokenStorage
    {
        private readonly Context _context;

        public TokenStorage(Context context)
        {
            _context = context;
        }

        public TokenStorage(){}

        public Token SelectByTokenValue(string authorization)
        {
            return _context.Token.Where(t => t.TokenValue == authorization).SingleOrDefault();
        } 
    }
}
