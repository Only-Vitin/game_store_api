using AutoMapper;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Services
{
    public class UserService
    {
        private readonly IUserDao _userDao;
        private readonly IMapper _mapper;
        private readonly IByCrypt _bcrypt;

        public UserService(IUserDao userDao, IMapper mapper, IByCrypt bcrypt)
        {
            _userDao = userDao;
            _mapper = mapper;
            _bcrypt = bcrypt;
        }

        public bool VerifyEmailOnDb(string email)
        {
            User userOnDb = _userDao.GetUserByEmail(email);
            if(userOnDb == null) return false;

            return true;
        }

        public bool VerifyUserId(int userId)
        {
            return _userDao.AnyUserById(userId);
        }

        public void AddBalance(User user, double value)
        {
            _userDao.AddValueToBalance(user, value);
            _userDao.SaveChanges();
        }

        public void RemoveBalance(User user, double value)
        {
            _userDao.RemoveValueFromBalance(user, value);
            _userDao.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _userDao.GetUserByEmail(email);
        }

        public User GetById(int userId)
        {
            return _userDao.GetUserById(userId);
        }

        public List<GetUserDto> Get()
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _userDao.GetAllUsers())
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }
            
            return usersDto;
        }

        public GetUserByIdDto GetByIdDto(int userId)
        {
            User user = _userDao.GetUserById(userId);
            return _mapper.Map<GetUserByIdDto>(user);
        }

        public GetUserDto Post(PostUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            user.Password = _bcrypt.EncryptPassword(user.Password);

            _userDao.AddUser(user);
            _userDao.SaveChanges();

            return _mapper.Map<GetUserDto>(user);
        }

        public void Put(PostUserDto updatedUser, User user)
        {
            updatedUser.Password = _bcrypt.EncryptPassword(updatedUser.Password);
            _userDao.UpdateUser(updatedUser, user);
            _userDao.SaveChanges();
        }

        public void Delete(User user)
        {
            _userDao.DeleteUser(user);
            _userDao.SaveChanges();
        }
    }
}
