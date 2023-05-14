using PizzaOrderAPI.Models;
using System.Security.Cryptography;
using PizzaOrderAPI.Enums;

namespace PizzaOrderAPI.Data.Mock
{
    public class MockUserRepository: IUserRepository
    {
        private readonly IList<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "Test1",
                PasswordHash = Convert.FromBase64String("DfaHxZvXk+/YM5vvf8abzqB203DbfQp4yrwpNXzmrfZrx1YUqlxD5e/8vxgSHmArGOk0gFRrDN9dZxG5FMrGYg=="),
                PasswordSalt = Convert.FromBase64String("TurXV38/se6DGQ2l6EJWOxn4FEJfJGBhCEcPAy6evYWHGuOlzNihozd6iXXY/7CMjpBfuNXVG4H3F2VNOH7TUbwd2u47Jo24ElJOEgUuDC3Xx7ztjK6ZB5eieq39j9cZ6XNbv+eqgpgp/9DVfqBFo41awPcewtCss6EeYKHIOBU="),
                Role = UserRole.GeneralManager
            },
            new User
            {
                Id = 2,
                UserName = "Test2",
                PasswordHash = Convert.FromBase64String("Nn483IV++aMuMp5pd2ApXhjkMSv2lSwXqwaRV6QdlEpZCmVm0Y4g/kEDXFL1VdEPi0lVz/dSrmyEj0f9RpqJoA=="),
                PasswordSalt = Convert.FromBase64String("RIFrPvYTBwFnCSvCTba0O/4ktMbDTfKtRbtkd5Dd3mac3HE8PqXpW2RNskeEaAcu8rcywolpqc5jb0inTVCAOYAiNWmZo2XICIzjv5xCW19+FjqXeKfJ4dv69wasFaWIoAdGh61oX1s8yVZWem6GQqn3Qo0MDDmf8eQPeen+oGU="),
                Role = UserRole.Manager
            },
            new User
            {
                Id =3,
                UserName = "AppUser",
                PasswordHash = Convert.FromBase64String("nhvKK5Helz3UCth3sRyfwPY3NhsSblmKovY4/EjohXthbfUPtsJgTwlVNmPPoLkcBa5QRz/4/LlFSwxVmhaklA=="),
                PasswordSalt = Convert.FromBase64String("x0wrDn4D3Ip2h/XLJ/IUFAdBEy2DrMOtJS62XAciB4zZGy5TC31q+wn9/u44GI14Xzjdnvz0aouXkMzJXm0EJ4vDdXa5zu/U9Zo/qLPc1iOwmcedW1JwBLfKrBXUZLO9prRcGJ7DRsAZAbwEnBh0rSCZoAKo4p2+I024es4usm4=")
            }
        };

        public User CreateUser(UserDTO newUser)
        {
            CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            return new User
            {
                Id = (!_users.Any()? 0: _users.Max(u => u.Id)) + 1,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        public User? LoginUser(string username, string password)
        {
            return _users.FirstOrDefault(u => 
                u.UserName == username && 
                VerifyPasswordHash(password, u.PasswordHash, u.PasswordSalt));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
