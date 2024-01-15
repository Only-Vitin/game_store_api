namespace game_store_api.Helper
{   
    public struct CustomMessage
    {
        public CustomMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
