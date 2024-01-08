using System.Linq;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Entities;
using game_store_api.Interfaces;
using game_store_api.Storage;

namespace game_store_api.Utils
{
    public class AuthHelper : Controller, IAuthHelper
    { 
        private static TokenStorage _tokenStorage;
        public AuthHelper(TokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public bool VerifyTokenOnDb()
        {
            string headersToken = Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault().Value.ToString();
            Token selectedToken = _tokenStorage.SelectByTokenValue(headersToken);
            if(selectedToken == null) return false;

            return true;
        }
    }
}
