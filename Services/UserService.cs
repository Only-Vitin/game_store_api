using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Service
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool VerifyEmailOnDb(PostUserDto userDto)
        {
            User anyUser = _context.User.Where(u => u.Email == userDto.Email).SingleOrDefault();
            if(anyUser == null) return false;

            return true;
        }

        public List<GetUserDto> GetUserService()
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _context.User)
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }

            return usersDto;
        }

        public GetUserDto AddUserOnDb(PostUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = BCryptNet.EnhancedHashPassword(user.Password, 13);

            _context.User.Add(user);
            _context.SaveChanges();

            GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);

            return userGetDto;
        }

        public void PutUserService(PostUserDto userDto,User user)
        {
            userDto.Password = BCryptNet.EnhancedHashPassword(userDto.Password, 13);
            _mapper.Map(userDto, user);
            _context.SaveChanges();
        }

        public void AddBalanceService(User selectedUser, double value)
        {
            selectedUser.Balance += value;
            _context.SaveChanges();
        }
    }
}
