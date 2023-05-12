using Microsoft.IdentityModel.Tokens;
using PizzaOrderAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace PizzaOrderAPI.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IUserRepository _userRepository;
        private readonly string _key;

        public JwtAuthenticationManager(IUserRepository userRepository, string key)
        {
            _userRepository = userRepository;
            _key = key;
        }

        public string Authenticate(string username, string password)
        {
            if(!_userRepository.LoginUser(username, password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
