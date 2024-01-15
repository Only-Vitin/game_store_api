using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Services
{
    public class GameService
    {
        private readonly IGameDao _gameDao;
        private readonly IMapper _mapper;

        public GameService(IGameDao gameDao, IMapper mapper)
        {
            _gameDao = gameDao;
            _mapper = mapper;
        }

        public List<GetGameDto> Get()
        {
            List<GetGameDto> gamesDto = new();
            foreach(Game game in _gameDao.GetAllGames())
            {
                GetGameDto getGameDto = _mapper.Map<GetGameDto>(game);
                gamesDto.Add(getGameDto);
            }

            return gamesDto;
        }

        public PostGameDto GetByIdDto(int gameId)
        {
            Game game = _gameDao.GetGameById(gameId);
            return _mapper.Map<PostGameDto>(game);
        }

        public Game GetById(int gameId)
        {
            return _gameDao.GetGameById(gameId);;
        }
        
        public GetGameDto Post(PostGameDto gameDto)
        {
            Game game = _mapper.Map<Game>(gameDto);
            _gameDao.AddGame(game);

            return _mapper.Map<GetGameDto>(game);
        }

        public void Put(PostGameDto updatedGame, Game game)
        {
            _gameDao.UpdateGame(updatedGame, game);
        }

        public void Delete(Game game)
        {
            _gameDao.DeleteGame(game);
        }
    }
}
