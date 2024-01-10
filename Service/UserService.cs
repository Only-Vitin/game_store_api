using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Service
{
    public class UserService
    {
        public static bool VerifyEmailOnDb(PostUserDto userDto, Context _context)
        {
            User anyUser = _context.User.Where(u => u.Email == userDto.Email).SingleOrDefault();
            if(anyUser == null) return false;

            return true;
        }

        public static List<GetUserDto> GetUserService(Context _context, IMapper _mapper)
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _context.User)
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }

            return usersDto;
        }

        public static GetUserDto AddUserOnDb(PostUserDto userDto, Context _context, IMapper _mapper)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = BCryptNet.EnhancedHashPassword(user.Password, 13);

            _context.User.Add(user);
            _context.SaveChanges();

            GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);

            return userGetDto;
        }

        public static void PutUserService(PostUserDto userDto,User selectedUser, Context _context, IMapper _mapper)
        {
            userDto.Password = BCryptNet.EnhancedHashPassword(userDto.Password, 13);
            _mapper.Map(userDto, selectedUser);
            _context.SaveChanges();
        }
    }
}
