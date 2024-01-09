using System.Linq;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Storage
{
    public class UserStorage : IUserStorage
    {
        private readonly Context _context;

        public UserStorage(Context context)
        {
            _context = context;
        }

        public User SelectById(int userId)
        {
            return _context.User.Where(u => u.UserId == userId).SingleOrDefault();
        }

        public User SelectByEmail(LoginDto login)
        {
            return _context.User.Where(u => u.Email == login.Email).Single();
        }

        public void DiscountGamePrice(User user, Game game)
        {
            user.Balance -= game.Price;
        }

        public void RemoveUser(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }
    }
}
