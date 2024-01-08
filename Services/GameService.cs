using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Service
{
    public class GameService
    { 
        public static List<GetGameDto> GetGameService(Context _context, IMapper _mapper)
        {
            List<GetGameDto> gamesDto = new();
            foreach(Game game in _context.Game)
            {
                GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
                gamesDto.Add(getGameDto);
            }

            return gamesDto;
        }
    }
}
