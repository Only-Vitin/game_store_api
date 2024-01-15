using BCryptNet = BCrypt.Net.BCrypt;

using game_store_api.Abstractions;

namespace game_store_api.Repository
{
    public class ByCrypt : IByCrypt
    {
        public bool ComparePasswords(string loginPassword, string dbHashPassword)
        {
            return BCryptNet.EnhancedVerify(loginPassword, dbHashPassword);
        }

        public string EncryptPassword(string password)
        {
            return BCryptNet.EnhancedHashPassword(password, 13);
        }
    }
}