using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserStorage
    {
        public User SelectById(int userId);
    }
}
