namespace game_store_api.Dto
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
