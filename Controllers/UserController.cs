using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Dto;
using game_store_api.Helper;
using game_store_api.Entities;
using game_store_api.Interfaces;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;
        private readonly IResponseHelper _respHelper;
        private readonly IUserService _userService;
        private readonly IUserStorage _userStorage;

        public UserController(IAuthHelper authHelper, IResponseHelper respHelper,
            IUserService userService, IMapper mapper, IUserStorage userStorage)
        {
            _authHelper = authHelper;
            _respHelper = respHelper;
            _userService = userService;
            _userStorage = userStorage;
            _mapper  = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser()
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            List<GetUserDto> usersDto = _userService.GetUserService();
            return Ok(usersDto);
        }
        
        [HttpGet("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult GetUserById(int userId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();

            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            GetUserByIdDto userDto = _mapper.Map<GetUserByIdDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] PostUserDto userDto)
        {
            _respHelper.AddDateHeaders(Response);

            if(_userService.VerifyEmailOnDb(userDto))
            {
                return Conflict(new CustomMessage("O email já existe no banco de dados"));
            }
            
            GetUserDto userGetDto = _userService.AddUserOnDb(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userGetDto.UserId }, userGetDto);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult PutUser(int userId, [FromBody] PostUserDto userDto)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();
            
            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            _userService.PutUserService(userDto, user);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult DeleteUser(int userId)
        {
            _respHelper.AddDateHeaders(Response);
            if(!_authHelper.VerifyTokenOnDb(Request)) return Unauthorized();
            
            User user = _userStorage.SelectById(userId);
            if(user == null) return NotFound();

            _userStorage.RemoveUser(user);

            return NoContent();
        }
    }
}
