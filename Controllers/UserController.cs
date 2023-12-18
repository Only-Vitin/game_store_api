using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;

using game_store_api.Models;
using game_store_api.Dto;
using System.Collections.Generic;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private Context _context;
        private IMapper _mapper;

        public UserController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            List<GetUserDto> usersDto = new();
            foreach(User user in _context.User)
            {
                GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);
                usersDto.Add(userGetDto);
            }
            return Ok(usersDto);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User selectedUser = _context.User.Where(i => i.Id == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            GetUserByIdDto selectedUserDto = _mapper.Map<GetUserByIdDto>(selectedUser);

            return Ok(selectedUserDto);
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] PostUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _context.User.Add(user);
            _context.SaveChanges();

            GetUserDto userGetDto = _mapper.Map<GetUserDto>(user);

            Response.Headers.Add("Date", $"{DateTime.Now}");
            return CreatedAtAction(nameof(GetUserById), new { Id = userGetDto.Id }, userGetDto);
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] PostUserDto userDto)
        {
            User selectedUser = _context.User.Where(user => user.Id == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            _mapper.Map(userDto, selectedUser);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            User selectedUser = _context.User.Where(user => user.Id == id).SingleOrDefault();
            Response.Headers.Add("Date", $"{DateTime.Now}");
            if(selectedUser == null) return NotFound();

            _context.Remove(selectedUser);
            _context.SaveChanges();

            return NoContent();
        }
    }
}