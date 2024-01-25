using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Abstractions;

namespace game_store_api.Repository
{
    public class UserDaoEfCore : IUserDao
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserDaoEfCore(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AnyUserById(int userId)
        {
            return _context.User.Any(u => u.UserId == userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User;
        }

        public User GetUserById(int userId)
        {
            return _context.User.Where(u => u.UserId == userId).SingleOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.Where(u => u.Email == email).SingleOrDefault();
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
        }

        public void UpdateUser(PostUserDto updatedUser, User user)
        {
            _mapper.Map(updatedUser, user);
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
        }

        public void AddValueToBalance(User user, double value)
        {
            user.Balance += value;
        }

        public void RemoveValueFromBalance(User user, double value)
        {
            user.Balance -= value;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
