using Microsoft.EntityFrameworkCore;

using game_store_api.Entities;

namespace game_store_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt){}

        public DbSet<Game> Game { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<PurchasedGames> PurchasedGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Token>().ToTable("Token");
            modelBuilder.Entity<PurchasedGames>().ToTable("PurchasedGames");

            modelBuilder.Entity<Token>().HasKey(t => t.TokenId);
            modelBuilder.Entity<User>().HasKey(t => t.UserId);
            modelBuilder.Entity<Game>().HasKey(t => t.GameId);
            modelBuilder.Entity<PurchasedGames>().HasKey(t => t.PurchasedGameId);

            modelBuilder.Entity<Token>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<PurchasedGames>()
                .HasOne(upg => upg.User)
                .WithMany(u => u.PurchasedGames)
                .HasForeignKey(upg => upg.UserId);

            modelBuilder.Entity<PurchasedGames>()
                .HasOne(upg => upg.Game)
                .WithMany(g => g.PurchasedGames)
                .HasForeignKey(upg => upg.GameId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
