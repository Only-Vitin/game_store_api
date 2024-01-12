using System.Collections.Generic;
using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface IPurchasedGamesService
    {
        public List<Game> GetById(int userId);
    }
}
