using Microsoft.AspNetCore.Http;

namespace game_store_api.Interfaces
{
    public interface IResponseHelper
    {    
        void AddDateHeaders(HttpResponse response);
        void AddTokenHeaders(HttpResponse responde, string token);
    }
}
