using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using game_store_api.Dto;
using game_store_api.Data;
using game_store_api.Utils;
using game_store_api.Service;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public LoginController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if(!LoginService.VerifyEmailOnDb(login, _context))
            {
                CustomMessage customMessage = new("O email não foi encontrado. Verifique suas credenciais ou registre-se para uma nova conta");
                return NotFound(customMessage);
            }

            User user = _context.User.Where(u => u.Email == login.Email).Single();
            
            if(!LoginService.VerifyPassword(login, user))
            {
                CustomMessage customMessage = new("Senha incorreta");
                return Unauthorized(customMessage);
            }
            
            string token = LoginService.CreateToken(user);
            LoginService.SaveTokenOnDb(user, token, _context);

            //
            GetUserDto getUserDto = _mapper.Map<GetUserDto>(user);
            Response.Headers.Add("Authorization", $"Bearer {token}");
            return Ok(getUserDto);
        }
    }
}
