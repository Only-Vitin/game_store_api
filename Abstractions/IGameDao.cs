using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface IGameDao
    {
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int gameId);
        void AddGame(Game game);
        void UpdateGame(PostGameDto updatedGame, Game game);
        void DeleteGame(Game game);
        void SaveChanges();
    }
}
