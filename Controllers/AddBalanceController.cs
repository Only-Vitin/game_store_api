using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddBalanceController : ControllerBase
    {
        private readonly IAuthHelper _authHelper;
        private readonly IResponseHelper _respHelper;
        private readonly IUserStorage _userStorage;
        private readonly IUserService _userService;

        public AddBalanceController(IAuthHelper authHelper, IResponseHelper respHelper,
            IUserService userService, IUserStorage userStorage)
        {
            _authHelper = authHelper;
            _respHelper = respHelper;
            _userService = userService;
            _userStorage = userStorage;
        }

        [HttpPost("user/{userId}/value/{value}")]
        [Authorize(Roles = "user")]
        public IActionResult AddBalanceByUserId(int userId, double value)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            _userService.AddBalanceService(user, value);

            return Ok(new CustomMessage("Valor adicionado com sucesso"));
        }
    }
}
