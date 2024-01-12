using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.ServicesInterfaces
{
    public interface IUserService
    {
        bool VerifyEmailOnDb(string email);
        void AddBalance(User user, double value);
        void RemoveBalance(User user, double value);
        User GetByEmail(string email);
        List<GetUserDto> Get();
        GetUserByIdDto GetByIdDto(int userId);
        User GetById(int userId);
        GetUserDto Post(PostUserDto userDto);
        void Put(PostUserDto updatedUser, User user);
        void Delete(User user);
    }
}