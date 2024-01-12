using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface IGameService
    {
        List<GetGameDto> Get();
        PostGameDto GetByIdDto(int gameId);
        Game GetById(int gameId);
        GetGameDto Post(PostGameDto gameDto);
        void Put(PostGameDto updatedGame, Game game);
        void Delete(Game game);
    }
}
