using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IJwt
    {
        public string EncodeToken(User user);
    }
}