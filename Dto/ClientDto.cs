using System.ComponentModel.DataAnnotations;

namespace game_store_api.Models
{
    public class ClientDto : UserDto
    {
        [Required(ErrorMessage = "Informe a sua idade")]
        [Range(10, 200, ErrorMessage = "A idade deve ser entre 10 e 200 anos")]
        public int Age { get; set; }

        [Required]
        public double Balance { get; set; }
    }
}
