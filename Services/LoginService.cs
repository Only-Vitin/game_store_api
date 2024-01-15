using System;
using AutoMapper;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class LoginService
    { 
        private readonly IMapper _mapper;
        private readonly ITokenStorage _tokenStorage;
        private readonly IByCrypt _bcrypt;
        private readonly IJwt _jwt;

        public LoginService(IMapper mapper, ITokenStorage tokenStorage, IByCrypt bcrypt, IJwt jwt)
        {
            _mapper = mapper;
            _tokenStorage = tokenStorage;
            _bcrypt = bcrypt;
            _jwt = jwt;
        }

        public bool VerifyPassword(LoginDto login, User user)
        {
            string dbHashPassword = user.Password;
            string loginPassword = login.Password;

            return _bcrypt.ComparePasswords(loginPassword, dbHashPassword);
        }

        public string CreateToken(User user)
        {
            return _jwt.EncodeToken(user);
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
