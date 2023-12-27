using AutoMapper;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDto, Game>();
            CreateMap<Game, GameDto>();
            CreateMap<Game, GetGameDto>();
        }
    } 
}
