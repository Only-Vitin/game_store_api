using System.Collections.Generic;
using game_store_api.Dto;
using game_store_api.Entities;

namespace game_store_api.Interfaces
{
    public interface IUserService
    {
        public List<GetUserDto> GetUserService();
        public GetUserDto AddUserOnDb(PostUserDto userDto);
        public bool VerifyEmailOnDb(PostUserDto userDto);
        public void PutUserService(PostUserDto userDto,User selectedUser);
        public void AddBalanceService(User selectedUser, double value);
    }
}
