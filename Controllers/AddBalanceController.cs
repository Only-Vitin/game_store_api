using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using game_store_api.Helper;
using game_store_api.Entities;

namespace game_store_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddBalance : ControllerBase
    { 
        private readonly AuthHelper _auth = new();

        [HttpPost("user/{userId}/value/{value}")]
        [Authorize(Roles = "user")]
        public IActionResult AddBalanceByUserId(int userId, double value)
        {
            HeadersHelper.AddDateOnHeaders(Response);
            if(!_auth.ValidToken(Request)) return Unauthorized();

            User selectedUser = _context.User.Where(u => u.UserId == userId).SingleOrDefault();
            if(selectedUser == null) return NotFound();

            selectedUser.Balance += value;
            _context.SaveChanges();
 
            return Ok(new CustomMessage("Valor adicionado com sucesso"));
        }
    }
}
