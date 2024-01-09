using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserStorage
    {
        User SelectById(int userId);
        void DiscountGamePrice(User selectedUser, Game selectedGame);
        User SelectByEmail(LoginDto login);
        void RemoveUser(User user);
    }
}
