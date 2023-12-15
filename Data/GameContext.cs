using Microsoft.EntityFrameworkCore;

using game_store_api.Models;

namespace web_api.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> opt) : base (opt){}

        public DbSet<Game> Game { get; set; }
    }
}