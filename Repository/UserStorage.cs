using AutoMapper;
using System.Linq;
using System.Collections.Generic;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Repository
{
    public class UserStorage : IUserStorage
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserStorage(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            _context.SaveChanges();
        }

        public void UpdateUser(PostUserDto updatedUser, User user)
        {
            _mapper.Map(updatedUser, user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public void AddValueToBalance(User user, double value)
        {
            user.Balance += value;
            _context.SaveChanges();
        }

        public void RemoveValueFromBalance(User user, double value)
        {
            user.Balance -= value;
            _context.SaveChanges();
        }
    }
}
