using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Abstractions
{
    public interface IUserDao
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(PostUserDto updatedUser, User user);
        void DeleteUser(User user);
        void AddValueToBalance(User user, double value);
        void RemoveValueFromBalance(User user, double value);
    }
}
