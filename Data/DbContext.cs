using Microsoft.EntityFrameworkCore;

using game_store_api.Models;

namespace web_api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base (opt){}

        public DbSet<Game> Game { get; set; }
        public DbSet<User> User { get; set; }

    }
}
