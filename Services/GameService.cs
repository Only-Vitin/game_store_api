using System.Collections.Generic;
using AutoMapper;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class GameService
    {
        private readonly IGameStorage _gameStorage;
        private readonly IMapper _mapper;

        public List<GetGameDto> Get()
        {
            List<GetGameDto> gamesDto = new();
            foreach(Game game in _gameStorage.GetAllGames())
            {
                GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
                gamesDto.Add(getGameDto);
            }

            return gamesDto;
        }
    }
}
