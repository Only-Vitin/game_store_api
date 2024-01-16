namespace game_store_api.Abstractions
{
    public interface IByCrypt
    {
        bool ComparePasswords(string loginPassword, string dbHashPassword);
        string EncryptPassword(string password);
    }
}
