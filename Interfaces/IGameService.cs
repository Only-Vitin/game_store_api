using System.Collections.Generic;

using game_store_api.Dto;

namespace game_store_api.Interfaces
{
    public interface IGameService
    {
        public List<GetGameDto> GetGameService();
    }
}
