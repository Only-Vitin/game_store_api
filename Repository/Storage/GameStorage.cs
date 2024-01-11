using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Repository.Storage
{
    public class GameStorage : IGameStorage
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GameStorage(AppDbContext context, IMapper mapper)
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
            _context.SaveChanges();
        }

        public void UpdateGame(PostGameDto updatedGame, Game game)
        {
            _mapper.Map(updatedGame, game);
            _context.SaveChanges();
        }

        public void DeleteGame(Game game)
        {
            _context.Remove(game);
            _context.SaveChanges();
        }
    }
}
