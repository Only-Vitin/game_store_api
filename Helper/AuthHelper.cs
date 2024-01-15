using System.Linq;
using Microsoft.AspNetCore.Http;

using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Helper
{
    public class AuthHelper
    {
        private readonly ITokenDao _tokenDao;

        public AuthHelper(ITokenDao tokenDao)
        {
            _tokenDao = tokenDao;
        }

        public bool ValidToken(HttpRequest request)
        {
            string authorization = request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();

            Token tokenOnDb = _tokenDao.GetTokenByValue(authorization);
            if(tokenOnDb == null) return false;

            return true;
        }
    }
}
