using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface IJwt
    {
        public string EncodeToken(User user);
    }
}
