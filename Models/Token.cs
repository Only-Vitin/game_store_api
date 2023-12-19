using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace game_store_api.Models
{
    public class Token
    {
        [Key]
        [Required]
        public int TokenId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public string TokenValue { get; set; }
    
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}