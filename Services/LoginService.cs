using System;
using AutoMapper;
using System.Text;
using System.Security.Claims;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;
using game_store_api.ServicesInterfaces;

namespace game_store_api.Services
{
    public class LoginService : ILoginService
    { 
        private readonly IMapper _mapper;
        private readonly ITokenStorage _tokenStorage;

        public LoginService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public bool VerifyPassword(LoginDto login, User user)
        {
            string dbHashPassword = user.Password;
            bool correctPassword = BCryptNet.EnhancedVerify(login.Password, dbHashPassword);

            return correctPassword;
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret.Word);

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

        public GetUserDto SaveTokenOnDb(User user, string token)
        {
            Token tokenClass = new()
            {
                TokenValue = $"Bearer {token}",
                UserId = user.UserId,
                ExpirationDate = DateTime.UtcNow.AddDays(10).ToString()
            };

            _tokenStorage.AddTokenOnDb(tokenClass);
        
            return _mapper.Map<GetUserDto>(user);
        }
    }
}
