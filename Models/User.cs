using System.ComponentModel.DataAnnotations;

namespace game_store_api.Models
{
    public abstract class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
