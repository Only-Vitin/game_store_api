//first implementation ready

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        public AddBalanceController(IAuthHelper authHelper, IResponseHelper respHelper)
        {
            _authHelper = authHelper;
            _respHelper = respHelper;
        }

        [HttpPost("user/{userId}/value/{value}")]
        [Authorize(Roles = "user")]
        public IActionResult AddBalanceByUserId(int userId, double value)
        {
            _respHelper.AddDateHeaders();

            if(!_authHelper.VerifyTokenOnDb()) return Unauthorized();

            User selectedUser = _userStorage.SelectById(userId);
            if(selectedUser == null) return NotFound();

            _userService.AddBalanceService(selectedUser, value);

            _respHelper.BodyMessage = "Valor adicionado com sucesso";
            return Ok(_respHelper.BodyMessage);
        }
    }
}
