using Microsoft.IdentityModel.Tokens;
using PizzaOrderAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using PizzaOrderAPI.Enums;

namespace PizzaOrderAPI.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAuthenticationManager(IUserRepository userRepository, 
                                        IConfiguration configuration,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Authenticate(string username, string password)
        {
            var user = _userRepository.LoginUser(username, password);

            if(user == null)
                return null;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role?.ToString()??string.Empty)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public (string?, string?) GetUser()
        {
            return (
                    _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name),
                    _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role));
        } 
    }
}
