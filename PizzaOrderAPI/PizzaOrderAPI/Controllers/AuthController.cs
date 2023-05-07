using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Models;
using PizzaOrderAPI.Auth;
using Microsoft.AspNetCore.Authorization;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
  
        public IActionResult Authenticate(User user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if(token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
