using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Models;
using PizzaOrderAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using PizzaOrderAPI.Data;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public AuthController(IUserRepository userRepository, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userRepository = userRepository;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult<User> CreateUser(UserDTO newUser)
        {
            return Ok(_userRepository.CreateUser(newUser));
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigins")]
        public IActionResult Authenticate(UserDTO user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if(token == null)
                return Unauthorized();
            return Ok(new {Token = token});
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUserRole()
        {
            var user = _jwtAuthenticationManager.GetUser();
            return Ok(new
            {
                User = user.Item1,
                Role = user.Item2,
            });
        }
    }
}
