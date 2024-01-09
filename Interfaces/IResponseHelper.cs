using Microsoft.AspNetCore.Http;

namespace game_store_api.Interfaces
{
    public interface IResponseHelper
    {    
        public void AddDateHeaders(HttpResponse response);
        public void AddTokenHeaders(HttpResponse responde, string token);
    }
}
