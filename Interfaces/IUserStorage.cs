using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserStorage
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmail(PostUserDto userDto);
        void AddUser(User user);
        void UpdateUser(PostUserDto updatedUser, User user);
        void DeleteUser(User user);
    }
}