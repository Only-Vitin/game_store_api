using AutoMapper;

using game_store_api.Dto;
using game_store_api.Models;

namespace web_api.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDto, Game>();
            CreateMap<Game, GameDto>();
        }
    } 
}