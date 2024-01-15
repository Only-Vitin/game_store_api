namespace game_store_api.Interfaces
{
    public interface IByCrypt
    {
        bool ComparePasswords(string loginPassword, string dbHashPassword);
        string EncryptPassword(string password);
    }
}