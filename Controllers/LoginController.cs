using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Dto;
using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserStorage _userStorage;
        private readonly IResponseHelper _respHelper;
        private readonly ILoginService _loginService;

        public LoginController(IMapper mapper, ILoginService loginService, IResponseHelper respHelper, IUserStorage userStorage)
        {
            _mapper = mapper;
            _userStorage = userStorage;
            _respHelper = respHelper;
            _loginService = loginService;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            _respHelper.AddDateHeaders(Response);

            if(!_loginService.VerifyEmailOnDb(login))
            {
                return NotFound(new CustomMessage("O email não foi encontrado. Verifique suas credenciais ou registre-se para uma nova conta"));
            }

            User user = _userStorage.SelectByEmail(login);
            
            if(!_loginService.VerifyPassword(login, user))
            {
                return Unauthorized(new CustomMessage("Senha incorreta"));
            }
            
            string token = _loginService.CreateToken(user);
            _loginService.SaveTokenOnDb(user, token);

            _respHelper.AddTokenHeaders(Response, token);
            GetUserDto getUserDto = _mapper.Map<GetUserDto>(user);
            return Ok(getUserDto);
        }
    }
}
