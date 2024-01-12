using System.Linq;
using Microsoft.AspNetCore.Http;

using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Helper
{
    public class AuthHelper
    {
        private readonly ITokenStorage _tokenStorage;

        public AuthHelper(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public bool ValidToken(HttpRequest request)
        {
            string authorization = request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();

            Token tokenOnDb = _tokenStorage.GetTokenByValue(authorization);
            if(tokenOnDb == null) return false;

            return true;
        }
    }
}
