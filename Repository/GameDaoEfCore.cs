using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Repository
{
    public class GameDaoEfCore : IGameDao
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GameDaoEfCore(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Game;
        }

        public Game GetGameById(int gameId)
        {
            return _context.Game.Where(g => g.GameId == gameId).SingleOrDefault();
        }

        public void AddGame(Game game)
        {
            _context.Game.Add(game);
        }

        public void UpdateGame(PostGameDto updatedGame, Game game)
        {
            _mapper.Map(updatedGame, game);
        }

        public void DeleteGame(Game game)
        {
            _context.Remove(game);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
