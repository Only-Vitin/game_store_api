using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;

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
            return Ok();
        }
    }
}