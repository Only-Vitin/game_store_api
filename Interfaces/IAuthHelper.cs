using Microsoft.AspNetCore.Http;

namespace game_store_api.Interfaces
{
    public interface IAuthHelper
    {
        bool VerifyTokenOnDb(HttpRequest request);
    }
}
