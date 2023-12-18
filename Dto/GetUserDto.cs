using System.ComponentModel.DataAnnotations;

namespace game_store_api.Models
{
    public class GetUserDto
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
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe se é adm")]
        public bool Adm { get; set; }
    }
}
