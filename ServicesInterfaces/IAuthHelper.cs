using Microsoft.AspNetCore.Http;

namespace game_store_api.ServicesInterfaces
{
    public interface IAuthHelper
    {
        bool ValidToken(HttpRequest request);
    }
}