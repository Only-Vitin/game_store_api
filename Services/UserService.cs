using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class UserService
    {
        private readonly IUserStorage _userStorage;
        private readonly IMapper _mapper;
        private readonly IByCrypt _bcrypt;

        public UserService(IUserStorage userStorage, IMapper mapper, IByCrypt bcrypt)
        {
            _userStorage = userStorage;
            _mapper = mapper;
            _bcrypt = bcrypt;
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
            user.Password = _bcrypt.EncryptPassword(user.Password);

            _userStorage.AddUser(user);

            return _mapper.Map<GetUserDto>(user);
        }

        public void Put(PostUserDto updatedUser, User user)
        {
            updatedUser.Password = _bcrypt.EncryptPassword(updatedUser.Password);
            _userStorage.UpdateUser(updatedUser, user);
        }

        public void Delete(User user)
        {
            _userStorage.DeleteUser(user);
        }
    }
}
