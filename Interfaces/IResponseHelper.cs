namespace game_store_api.Interfaces
{
    public interface IResponseHelper
    {
        public string BodyMessage { get; set; }
        
        public void AddDateHeaders();
    }
}
