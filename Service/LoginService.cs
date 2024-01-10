using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Entities;

namespace game_store_api.Service
{
    public class LoginService
    { 
        public static bool VerifyEmailOnDb(LoginDto login, Context _context)
        {
            User anyUser = _context.User.Where(u => u.Email == login.Email).SingleOrDefault();
            if(anyUser == null) return false;

            return true;
        }

        public static bool VerifyPassword(LoginDto login, User user)
        {
            string dbHashPassword = user.Password;
            bool correctPassword = BCryptNet.EnhancedVerify(login.Password, dbHashPassword);

            return correctPassword;
            
        }

        public static string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, user.Email.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static User SaveTokenOnDb(User user, string token, Context _context)
        {
            Token tokenClass = new()
            {
                TokenValue = $"Bearer {token}",
                UserId = user.UserId,
                ExpirationDate = DateTime.UtcNow.AddDays(10).ToString()

            };

            _context.Token.Add(tokenClass);
            _context.SaveChanges();
            
            return user;
        }
    }
}
