namespace game_store_api.Dto
{
    public class GetGameDto
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public string Description { get; set; }
        public bool Over18 { get; set; }
        public double Price { get; set; }
    }
}
