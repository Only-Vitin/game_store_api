using Microsoft.AspNetCore.Http;

namespace game_store_api.Interfaces
{
    public interface IAuthHelper
    {
        public bool VerifyTokenOnDb(HttpRequest request);
    }
}
