using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Services;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddBalance : ControllerBase
    {
        private readonly AuthHelper _auth;
        private readonly UserService _userService;

        public AddBalance(AuthHelper auth, UserService userService)
        {
            _auth = auth;
            _userService = userService;
        }

        [HttpPost("user/{userId}/value/{value}")]
        [Authorize(Roles = "user")]
        public IActionResult AddBalanceByUserId(int userId, double value)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            User user = _userService.GetById(userId);
            if(user == null) return NotFound();

            _userService.AddBalance(user, value);
 
            return Ok(new CustomMessage("Valor adicionado com sucesso"));
        }
    }
}
