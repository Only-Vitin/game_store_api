using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using game_store_api.Storage;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Utils
{
    public class AuthHelper : Controller, IAuthHelper
    { 
        private static TokenStorage _tokenStorage;
        
        public AuthHelper(TokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public bool VerifyTokenOnDb(HttpRequest request)
        {
            string headersToken = request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            Token selectedToken = _tokenStorage.SelectByTokenValue(headersToken);
            if(selectedToken == null) return false;

            return true;
        }
    }
}
