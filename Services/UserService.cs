using AutoMapper;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;
using game_store_api.ServicesInterfaces;

namespace game_store_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserStorage _userStorage;
        private readonly IMapper _mapper;

        public UserService(IUserStorage userStorage, IMapper mapper)
        {
            _userStorage = userStorage;
            _mapper = mapper;
        }

        public bool VerifyEmailOnDb(string email)
        {
            User userOnDb = _userStorage.GetUserByEmail(email);
            if(userOnDb == null) return false;

            return true;
        }

        public void AddBalance(User user, double value)
        {
            _userStorage.AddValueToBalance(user, value);
        }

        public void RemoveBalance(User user, double value)
        {
            _userStorage.RemoveValueFromBalance(user, value);
        }

        public User GetByEmail(string email)
        {
            return _userStorage.GetUserByEmail(email);
        }

        public List<GetUserDto> Get()
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _userStorage.GetAllUsers())
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }
            
            return usersDto;
        }

        public GetUserByIdDto GetByIdDto(int userId)
        {
            User user = _userStorage.GetUserById(userId);
            return _mapper.Map<GetUserByIdDto>(user);
        }

        public User GetById(int userId)
        {
            return _userStorage.GetUserById(userId);
        }

        public GetUserDto Post(PostUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = BCryptNet.EnhancedHashPassword(user.Password, 13);

            _userStorage.AddUser(user);

            return _mapper.Map<GetUserDto>(user);
        }

        public void Put(PostUserDto updatedUser, User user)
        {
            updatedUser.Password = BCryptNet.EnhancedHashPassword(updatedUser.Password, 13);
            _userStorage.UpdateUser(updatedUser, user);
        }

        public void Delete(User user)
        {
            _userStorage.DeleteUser(user);
        }
    }
}
