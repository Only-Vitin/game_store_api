using AutoMapper;
using System.Linq;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Storage
{
    public class GameStorage : IGameStorage
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public GameStorage(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Game SelectById(int gameId)
        {
            return _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
        }

        public void AddGame(Game game)
        {
            _context.Game.Add(game);
            _context.SaveChanges();
        }

        public void PutGame(PostGameDto gameDto, Game game)
        {
            _mapper.Map(gameDto, game);
            _context.SaveChanges();
        }
        public void RemoveGame(Game game)
        {
            _context.Remove(game);
            _context.SaveChanges();
        }
    }
}
