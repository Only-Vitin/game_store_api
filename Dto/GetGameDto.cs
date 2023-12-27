using System.ComponentModel.DataAnnotations;

namespace game_store_api.Dto
{
    public class GetGameDto
    {
        [Key]
        [Required]
        public int GameId { get; set; }
        
        [Required(ErrorMessage = "Informe o nome do jogo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o gênero do jogo")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Informe a plataforma do jogo")]
        public string Platform { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Por favor informe a descrição do jogo")]
        public string Description { get; set; }

        [Required(ErrorMessage = "É necessário informar se o jogo é para maiores de 18")]
        public bool Over18 { get; set; }
        
        [Required(ErrorMessage = "Informe o preço do jogo")]
        public double Price { get; set; }
    }
}
