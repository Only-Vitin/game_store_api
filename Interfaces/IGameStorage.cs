using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IGameStorage
    {
        public Game SelectById(int gameId);
        public void AddGame(Game game);
        public void PutGame(PostGameDto gameDto, Game game);
        public void RemoveGame(Game game);
    }
}
