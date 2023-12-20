using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using game_store_api.Models;
using Microsoft.IdentityModel.Tokens;
using web_api.Data;

namespace game_store_api.Utils
{   
    public class ValidToken
    {
        public bool VerifyToken(string token, Context _context)
        {
            Token dbToken = _context.Token.Where(t => t.TokenValue.ToString() == token).SingleOrDefault();
            if(dbToken == null) return false;
            
            return true;
        }
    }
}