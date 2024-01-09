using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Service
{
    public class GameService : IGameService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public GameService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetGameDto> GetGameService()
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
