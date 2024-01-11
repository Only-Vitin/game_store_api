using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Dto;
using game_store_api.Helper;
using game_store_api.Services;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService = new();
        private readonly LoginService _loginService = new();

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            HeadersHelper.AddDateOnHeaders(Response);

            User user = _userService.GetByEmail(login.Email);
            if(user == null)
            {
                CustomMessage message = new("O email não foi encontrado. Verifique suas credenciais ou registre-se para uma nova conta");
                return NotFound(message);
            }

            if(!_loginService.VerifyPassword(login, user)) return Unauthorized(new CustomMessage("Senha incorreta"));

            string token = _loginService.CreateToken(user);
            GetUserDto userDto = _loginService.SaveTokenOnDb(user, token);

            HeadersHelper.AddAuthorizationOnHeaders(Response, token);
            return Ok(userDto);
        }
    }
}
