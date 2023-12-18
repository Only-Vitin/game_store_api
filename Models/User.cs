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

        [Required(ErrorMessage = "Informe a sua idade")]
        [Range(10, 200, ErrorMessage = "A idade deve ser entre 10 e 200 anos")]
        public int Age { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe se é adm")]
        public bool Adm { get; set; }
    }
}
