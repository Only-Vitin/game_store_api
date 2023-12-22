using AutoMapper;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Profiles
{
   public class UserProfile : Profile
   {
      public UserProfile()
      {
         CreateMap<PostUserDto, User>();
         CreateMap<User, GetUserDto>();
         CreateMap<User, GetUserByIdDto>();
      }
   }
}
