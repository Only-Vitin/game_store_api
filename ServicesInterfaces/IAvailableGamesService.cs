using System.Collections.Generic;

using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface IAvailableGamesService
    {
        List<Game> GetById(int userId);
    }
}
