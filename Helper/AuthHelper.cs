using System.Linq;
using Microsoft.AspNetCore.Http;

using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Helper
{
    public class AuthHelper
    { 
        private readonly AppDbContext _context;

        public AuthHelper(){}
        public AuthHelper(AppDbContext context)
        {
            _context = context;
        }

        public bool ValidToken(HttpRequest request)
        {
            string authorization = request.Headers.Where(h => h.Key.ToString().Equals("Authorization")).SingleOrDefault().Value.ToString();

            Token tokenOnDb = _context.Token.Where(t => t.TokenValue == authorization).SingleOrDefault();
            if(tokenOnDb == null) return false;

            return true;
        }
    }
}
