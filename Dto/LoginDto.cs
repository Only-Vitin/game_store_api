using System.ComponentModel.DataAnnotations;

namespace game_store_api.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Informe seu email")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [StringLength(500)]
        public string Password { get; set; }
    }
}
