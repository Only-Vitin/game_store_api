using web_api.Data;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Dto;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public string Login([FromBody] LoginDto infoLogin)
        {
            return "";
        }
    }
}