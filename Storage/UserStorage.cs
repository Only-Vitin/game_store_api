using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_api.Data;
using game_store_api.Entities;

namespace game_store_api.Storage
{
    public class UserStorage
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
    }
}