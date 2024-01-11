using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Services
{
    public class GameService
    {
        private readonly IGameStorage _gameStorage;
        private readonly IMapper _mapper;

        public GameService(){}
        public GameService(IGameStorage gameStorage, IMapper mapper)
        {
            _gameStorage = gameStorage;
            _mapper = mapper;
        }

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

        public PostGameDto GetByIdDto(int gameId)
        {
            Game game = _gameStorage.GetGameById(gameId);
            return _mapper.Map<PostGameDto>(game);
        }

        public Game GetById(int gameId)
        {
            return _gameStorage.GetGameById(gameId);;
        }
        
        public GetGameDto Post(PostGameDto gameDto)
        {
            Game game = _mapper.Map<Game>(gameDto);
            _gameStorage.AddGame(game);

            return _mapper.Map<GetGameDto>(game);
        }

        public void Put(PostGameDto updatedGame, Game game)
        {
            _gameStorage.UpdateGame(updatedGame, game);
        }

        public void Delete(Game game)
        {
            _gameStorage.DeleteGame(game);
        }
    }
}
