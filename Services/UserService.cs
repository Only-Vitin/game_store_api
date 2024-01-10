using AutoMapper;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class UserService
    {
        private readonly IUserStorage _userContext;
        private readonly IMapper _mapper;

        public UserService(){}
        public UserService(IUserStorage userContext, IMapper mapper)
        {
            _userContext = userContext;
            _mapper = mapper;
        }

        public bool VerifyEmailOnDb(PostUserDto userDto)
        {
            User userOnDb = _userContext.GetUserByEmail(userDto);
            if(userOnDb == null) return false;

            return true;
        }

        public List<GetUserDto> Get()
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _userContext.GetAllUsers())
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }
            
            return usersDto;
        }

        public GetUserByIdDto GetByIdDto(int userId)
        {
            User user = _userContext.GetUserById(userId);
            return _mapper.Map<GetUserByIdDto>(user);
        }

        public User GetById(int userId)
        {
            return _userContext.GetUserById(userId);
        }

        public GetUserDto Post(PostUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = BCryptNet.EnhancedHashPassword(user.Password, 13);

            _userContext.AddUser(user);

            GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
            return userGetDto;
        }

        public void Put(PostUserDto updatedUser, User user)
        {
            updatedUser.Password = BCryptNet.EnhancedHashPassword(updatedUser.Password, 13);
            _userContext.UpdateUser(updatedUser, user);
        }

        public void Delete(User user)
        {
            _userContext.DeleteUser(user);
        }
    }
}
