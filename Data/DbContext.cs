using Microsoft.EntityFrameworkCore;

using game_store_api.Models;

namespace web_api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base (opt){}

        public DbSet<Game> Game { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Token>().ToTable("Token");

            modelBuilder.Entity<Token>()
            .HasKey(t => t.TokenId);
            modelBuilder.Entity<User>()
            .HasKey(t => t.UserId);
            modelBuilder.Entity<Game>()
            .HasKey(t => t.GameId);

            modelBuilder.Entity<Token>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
