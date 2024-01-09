using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IGameStorage
    {
        Game SelectById(int gameId);
        void AddGame(Game game);
        void PutGame(PostGameDto gameDto, Game game);
        void RemoveGame(Game game);
    }
}
