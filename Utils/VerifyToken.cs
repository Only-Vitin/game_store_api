using System.Linq;

using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Utils
{
    public class VerifyToken
    { 
        public static bool VerifyTokenOnDb(string authorization, Context _context)
        { 
            Token anyToken = _context.Token.Where(t => t.TokenValue == authorization).SingleOrDefault();
            if(anyToken == null) return false;

            return true;
        }
    }
}
