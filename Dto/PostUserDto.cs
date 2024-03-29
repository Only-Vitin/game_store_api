using System.ComponentModel.DataAnnotations;

namespace game_store_api.Dto
{
    public class PostUserDto
    { 
        [Required(ErrorMessage = "Informe seu nome")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe a sua idade")]
        [Range(0, 200, ErrorMessage = "A idade deve ser entre 10 e 200 anos")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [StringLength(500)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Informe se é admin ou user")]
        public string Role { get; set; }
    }
}
