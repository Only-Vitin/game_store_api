using System.Linq;
using web_api.Data;
using System.Collections.Generic;
using AutoMapper;

using game_store_api.Dto;
using game_store_api.Models;
namespace game_store_api.Service
{
    public class UserService
    {
        public bool VerifyEmailOnDb(PostUserDto userDto, Context _context)
        {
            User anyUser = _context.User.Where(user => user.Email == userDto.Email).SingleOrDefault();
            if(anyUser != null) return false;

            return true;
        }

        public List<GetUserDto> GetUserService(Context _context, IMapper _mapper)
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _context.User)
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }

            return usersDto;
        }
    }
}