using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface IGameDao
    {
        public IEnumerable<Game> GetAllGames();
        public Game GetGameById(int gameId);
        public void AddGame(Game game);
        public void UpdateGame(PostGameDto updatedGame, Game game);
        public void DeleteGame(Game game);
    }
}
