using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game_store_api.Entities
{
    public class PurchasedGames
    {
        [Key]
        [Required]
        public int PurchasedGameId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GameId { get; set; }
    
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
