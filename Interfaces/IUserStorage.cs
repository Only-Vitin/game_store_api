using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserStorage
    {
        public User SelectById(int userId);
        public void DiscountGamePrice(User selectedUser, Game selectedGame);
        public User SelectByEmail(LoginDto login);
        public void RemoveUser(User user);
    }
}
