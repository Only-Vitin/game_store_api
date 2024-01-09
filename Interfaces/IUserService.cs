using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserService
    {
        List<GetUserDto> GetUserService();
        GetUserDto AddUserOnDb(PostUserDto userDto);
        bool VerifyEmailOnDb(PostUserDto userDto);
        void PutUserService(PostUserDto userDto,User selectedUser);
        void AddBalanceService(User selectedUser, double value);
    }
}
