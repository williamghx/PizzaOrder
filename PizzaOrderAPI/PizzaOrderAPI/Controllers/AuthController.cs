using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderAPI.Models;
using PizzaOrderAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        [EnableCors("AllowSpecificOrigins")]
        public IActionResult Authenticate(User user)
        {
            var token = _jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if(token == null)
                return Unauthorized();
            return Ok(new {Token = token});
        }
    }
}
