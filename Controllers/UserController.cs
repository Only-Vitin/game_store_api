using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.ServicesInterfaces;

namespace game_store_api.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthHelper _auth;
        private readonly IUserService _userService;

        public UserController(IAuthHelper auth, IUserService userService)
        {
            _auth = auth;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser()
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            List<GetUserDto> usersDto = _userService.Get();
            return Ok(usersDto);
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetUserById(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            GetUserByIdDto userDto = _userService.GetByIdDto(userId);
            if(userDto == null) return NotFound();

            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] PostUserDto userDto)
        {
            HeadersHelper.AddDateOnHeaders(Response);

            if(_userService.VerifyEmailOnDb(userDto.Email))
            {
                return Conflict(new CustomMessage("O email já existe no banco de dados"));
            }
            GetUserDto userGetDto = _userService.Post(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userGetDto.UserId }, userGetDto);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult PutUser(int userId, [FromBody] PostUserDto updatedUser)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            User user = _userService.GetById(userId);
            if(user == null) return NotFound();

            _userService.Put(updatedUser, user);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult DeleteUser(int userId)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();
            
            User user = _userService.GetById(userId);
            if(user == null) return NotFound();

            _userService.Delete(user);

            return NoContent();
        }
    }
}
